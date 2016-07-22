
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using TPDigital.Data_Access_Layer.Data_Access_Layer;
using TPDigital.Data_Access_Layer.Data_View_Model;
using RestSharp;
using RestSharp.Authenticators;

namespace TPDigital.Controllers
{
    [MyAuthorizeAttribute(Roles = "管理员,顾客")]
    //1 订单具体列出后才可以展示具体内容太
    //2 进入具体订单内容显示界面才可以通过一个点击操作，跳转物流信息查询界面
    public class OrderController : Controller
    {
        /////////////////测试

        public ActionResult test()
        {

            return View();

        }


        /////////////////////




        // GET: Order
        //按时间先后顺序列出订单
        //通过订单编号(order_id)获取相应订单的订单状态(package_state_id)、订单总价(total_price)、订单日期(add_date)、
        //快递单号(express_id)、收货人信息(shopping_receviver_id)、商品(product_id)
        public ActionResult Index()
        {
            if (User.Identity.Name != "")
            {
                ViewBag.state = 1;
            }
            else
            {
                ViewBag.state = 0;
            }
            var user = User_DAL.getTpUserByID(decimal.Parse(User.Identity.Name));
            var orderList = Order_DAL.getListByUser(user);

            ViewBag.orderList = orderList;
            ViewBag.user = user;
            ViewBag.categoryList = Category_DAL.getAll();
            return View();
        }

        //创建订单 从前端得到商品id列表，数量列表，收货人id
        //通过totalPrice receiver_id userID创建order
        //并且比对购物车，删除相同商品id的购物车
        //返回是否创建成功
        [HttpPost]
        public ActionResult CreateOrder(FormCollection fc, List<decimal> productIDList, List<decimal> quantityList, decimal receiverId)
        {
            if (User.Identity.Name != "")
            {
                ViewBag.state = 1;
            }
            else
            {
                ViewBag.state = 0;
            }
            try
            {
                //得到user编号
                decimal userID = decimal.Parse(User.Identity.Name);
                var productList = Product_DAL.getListByIDList(productIDList);

                //得到总价
                decimal totalPrice = 0;
                for (int i = 0; i < productList.Count; i++)
                {
                    totalPrice += productList[i].Price * quantityList[i];
                }
                using (var ts = new TransactionScope())
                {
                    // 创建订单
                    decimal orderID = Order_DAL.createTP_ORDER(totalPrice, receiverId, userID);
                    // 创建商品orderProduct
                    for (int i = 0; i < productList.Count; i++)
                    {
                        if (!Order_Product_DAL.Create(orderID, productList[i], quantityList[i]))
                        {
                            ViewBag.flag = 0;
                            return new RedirectResult("/Error");
                        }

                    }
                    //比对购物车，删除相同商品id的购物车
                    foreach (var productID in productIDList)
                    {
                        var shoppingCart = ShoppingCart_DAL.getByProductIDUserID(productID, userID);
                        if (shoppingCart != null)
                        {
                            if (!ShoppingCart_DAL.Delete(shoppingCart))
                            {
                                ViewBag.flag = 0;
                                return new RedirectResult("/Error");
                            }

                        }

                    }
                    ViewBag.orderID = orderID;
                    ts.Complete();
                }
                ViewBag.productList = productList;
                ViewBag.quantityList = quantityList;
                ViewBag.categoryList = Category_DAL.getAll();
                ViewBag.receiver = new Receiver(Receiver_DAL.getByID(receiverId));
                ViewBag.flag = 1;


                return View();
            }
            catch
            {
                ViewBag.flag = 0;
                return new RedirectResult("/Error");
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection fc, List<decimal> shoppingCartIDs)
        {
            if (User.Identity.Name != "")
            {
                ViewBag.state = 1;
            }
            else
            {
                ViewBag.state = 0;
            }
            try
            {
                var user = User_DAL.getTpUserByID(decimal.Parse(User.Identity.Name));
                var receiverList = Receiver_DAL.getListByUser(user);
                ViewBag.receiverList = receiverList;

                //获得order_product list
                List<Order_Product> orderProductList = new List<Order_Product>();
                if (shoppingCartIDs != null)
                {
                    //从购物车
                    foreach (decimal id in shoppingCartIDs)
                    {
                        var shoppingCart = ShoppingCart_DAL.getByID(id);
                        Order_Product orderProduct = new Order_Product();
                        /////
                        orderProduct.Product = shoppingCart.Product;
                        orderProduct.setImageUrl();
                        ////
                        orderProduct.Price = shoppingCart.Product.Price;
                        orderProduct.ProductID = shoppingCart.Product.ID;
                        orderProduct.Quantity = shoppingCart.Quantity;
                        orderProductList.Add(orderProduct);
                    }
                }
                else
                {
                    //从商品
                    decimal productID = decimal.Parse(Request.Form["product_id"]);
                    var product = Product_DAL.getByID(productID);
                    Order_Product orderProduct = new Order_Product();
                    orderProduct.Price = product.Price;
                    orderProduct.ProductID = productID;
                    /////
                    orderProduct.Product = product;
                    orderProduct.setImageUrl();
                    ////
                    orderProduct.Quantity = decimal.Parse(Request.Form["quantity"]);
                    orderProductList.Add(orderProduct);
                }
                ViewBag.orderProductList = orderProductList;
                //得到总价
                decimal totalPrice = 0;
                foreach (var item in orderProductList)
                {
                    if (item.Quantity > item.Product.Inventory)
                        return new RedirectResult("/Error");
                    totalPrice += item.Price * item.Quantity;
                }
                ViewBag.totalPrice = totalPrice;

                ViewBag.categoryList = Category_DAL.getAll();

                ViewBag.provinces = Location_DAL.getProvince();

                return View();
            }
            catch
            {
                return new RedirectResult("/Error");
            }

        }

        //显示快递信息
        //从电商平台内部数据库中通过订单中的快递编号(express_id)获取快递公司(name)、单号(check_number)。

        public ActionResult ShowExInf(decimal id)
        {
            if (User.Identity.Name != "")
            {
                ViewBag.state = 1;
            }
            else
            {
                ViewBag.state = 0;
            }
            var express = Express_DAL.getByID(id);
            ViewBag.categoryList = Category_DAL.getAll();

            return View();
        }

        //支付订单界面
        //
        [HttpPost]
        public int PayOrder(FormCollection fc, decimal id)
        {
            var order = Order_DAL.getTpOrderByID(id);
            decimal userID = decimal.Parse(User.Identity.Name);
            if (order.USER_ID != userID)
                return 0;
            order.PAYMENT_STATE_ID = State_DAL.getPaymentStateByName("已付款").ID;
            SendEmail(id);
            Order_DAL.update(order);
            return 1;
        }


        [HttpPost]
        public int ConfirmOrder(FormCollection fc, decimal orderID)
        {
            var order = Order_DAL.getTpOrderByID(orderID);
            if (order == null)
                return 0;
            order.PACKAGE_STATE_ID = State_DAL.getPackageStateByName("已签收").ID;
            var updateState = Order_DAL.update(order);
            return updateState ? 1 : 0;
        }

        [HttpPost]
        public int AddComment(decimal order_id, decimal product_id, string content, decimal star)
        {
            decimal userID = decimal.Parse(User.Identity.Name);
            var order = Order_DAL.getTpOrderByID(order_id);
            if (order.PACKAGE_STATE_ID != State_DAL.getPackageStateByName("已签收").ID ||
                order.PAYMENT_STATE_ID != State_DAL.getPaymentStateByName("已付款").ID)
                return 0;
            bool insertStatus = Comment_DAL.Insert(product_id, order_id, userID, star, content);
            return insertStatus ? 1 : 0;
        }

        public IRestResponse SendEmail(decimal id)
        {
            var useid = Order_DAL.getByID(Convert.ToDecimal(id)).UserID;
            var email = User_DAL.getByID(useid).Email;
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                               "key-ab51a2f4b9f1aa3f11c165e1a50cbdce");
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                 "notify.picfood.cn", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Excited User <admin@notify.picfood.cn>");
            request.AddParameter("to", "15221628785@163.com");

            //request.AddParameter("to", email);

            request.AddParameter("subject", "订单通知");
            //string Context = "";
            var order = Order_DAL.getByID(Convert.ToDecimal(id));
            if (order == null) return null;
            order.addOrderProductList();
            string BodyStart = System.IO.File.ReadAllText(Server.MapPath("emailstart.html"));
            string BodyEnd = System.IO.File.ReadAllText(Server.MapPath("emailend.html"));

            BodyStart += ("<h1>尊敬的" + order.UserName +
                ":</h1><h1>\n我们已收到您的订单！\n</h1>" +
                "<table align=\"center\" border=\"1\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"border - collapse: collapse; \">" +
                "<tr><td><h1>商品名称</h1></td><td><h1>商品价格</h1></td><td><h1>商品数量</h1></td></tr>");

            foreach (var pro in order.OrderProductList)
            {
                BodyStart += ("<tr><td><h1>" + pro.Product.Name + "</h1></td><td><h1>" + pro.Product.Price + "元</h1></td><td><h1>" + pro.Quantity + "件</h1></td></tr>");
            }
            BodyStart += "</table>";
            BodyStart += ("<td><h1>总价: " + order.TotalPrice + "元<h1></td>");

            BodyStart += BodyEnd;
            request.AddParameter("html", BodyStart);
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }

}