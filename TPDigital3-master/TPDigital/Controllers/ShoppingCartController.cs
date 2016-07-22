using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPDigital.Models;
using System.Data.Entity;

using TPDigital.Data_Access_Layer.Data_Access_Layer;
using TPDigital.Data_Access_Layer.Data_View_Model;

namespace TPDigital.Controllers
{
    [MyAuthorizeAttribute(Roles = "顾客")]
    public class ShoppingCartController : Controller
    {
        
        // 购物车列表
        //GET
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
            User user = User_DAL.getByID(Decimal.Parse(User.Identity.Name));
            var shoppingCartlist = ShoppingCart_DAL.getAll(user.ID);
            List<Image> imageList = new List<Image>();
            foreach (var shoppingcart in shoppingCartlist)
            {
                imageList.Add(Product_DAL.getImageListByID(shoppingcart.Product.ID)[0]);
            }
            ViewBag.categoryList = Category_DAL.getAll();
            ViewBag.imageList = imageList;
            return View(shoppingCartlist);
        }

        //加入购物车
        [HttpPost]
        public int AddToShoppingCart()
        {
            User user = User_DAL.getByID(Decimal.Parse(User.Identity.Name));
            ShoppingCart newShoppingCart = new ShoppingCart();
            newShoppingCart.UserID = user.ID;
            newShoppingCart.Quantity = Decimal.Parse(Request.Form["quantity"]);
            newShoppingCart.Product = Product_DAL.getByMyID(Decimal.Parse(Request.Form["productID"]));
            if (newShoppingCart.Product == null)
                return 0;//前端返回过来的商品ID有误
            decimal shoppingCartID = ShoppingCart_DAL.getShoppingCartIDByUserProduct(newShoppingCart.UserID, newShoppingCart.Product.ID);
            if (shoppingCartID == -1)
                return ShoppingCart_DAL.Insert(newShoppingCart);
            else if (shoppingCartID == -2)
                return 0;
            else
                return ShoppingCart_DAL.AddQuantity(shoppingCartID, (int)newShoppingCart.Quantity);

        }

        //去结算
        [HttpPost]
        public ActionResult PlaceOrder()
        {
            if (User.Identity.Name != "")
            {
                ViewBag.state = 1;
            }
            else
            {
                ViewBag.state = 0;
            }
            User user = User_DAL.getByID(Decimal.Parse(User.Identity.Name));
            var shoppingCartIDsTmp = Request.Form["shoppingCartIDs"];
            string[] shoppingCartIDs = null;
            if (shoppingCartIDsTmp == null)
                shoppingCartIDs = shoppingCartIDsTmp.Split(',');
            var shoppingCartList = new List<ShoppingCart>();
            if (shoppingCartIDs == null)
                return View();
            foreach (string id in shoppingCartIDs)
            {
                shoppingCartList.Add(ShoppingCart_DAL.getByID(Decimal.Parse(id)));
            }
            ViewBag.shoppingCartList = shoppingCartList;
            ViewBag.receiverList = Receiver_DAL.getAll(user.ID);
            ViewBag.categoryList = Category_DAL.getAll();
            return View();
        }
        
        //删除商品(单个或多个都用表单的多选传）
        [HttpPost]
        public int DeleteSome()
        {
            var shoppingCartIDsTmp = Request.Form["shoppingCartIDs"];
            string[] shoppingCartIDs = null;
            if (shoppingCartIDsTmp != null)
                shoppingCartIDs = shoppingCartIDsTmp.Split(',');
            if (shoppingCartIDs != null)
            {
                foreach (string shoppingCartID in shoppingCartIDs)
                {
                    if (!ShoppingCart_DAL.Delete(Decimal.Parse(shoppingCartID)))
                        return 0;
                }
            }
            return 1;
        }


        //删除商品（单个）
        //GET
        public int Delete(int id)
        {
            if (!ShoppingCart_DAL.Delete((decimal)id))
                return 0;
            return 1;
        }


        //增加/减少购物车商品,参数add为1时表增加,为0表减少
        [HttpPost]
        public int Edit(int count)
        {
            return ShoppingCart_DAL.UpdateQuantity(Decimal.Parse(Request.Form["shoppingCartID"]), count);
        }

    }
}