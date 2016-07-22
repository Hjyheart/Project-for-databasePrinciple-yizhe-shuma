using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using TPDigital.Data_Access_Layer.Data_View_Model;
using TPDigital.Data_Access_Layer.Data_Access_Layer;
using RestSharp;
using RestSharp.Authenticators;

namespace TPDigital.Controllers
{
    public class ProductController : Controller
    {
        //public static SortedSet<HotProduct> hotProducts = new SortedSet<HotProduct>(new ProductComparer());
        //public static int n = 0;
        //public static bool inital = false;
        // GET: Product
        //列出热门商品
        //每个商品包含信息：图片、商品名称、价格
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
            if(!HotProducts.inital)
            {
                var pros = Product_DAL.getListBySize(4);
                if (pros != null)
                {
                    HotProducts.inital = true;
                    foreach (var prop in pros)
                    {
                        var p = new HotProduct(prop.ID);
                        //p.clickNum = n++;
                        HotProducts.hotProducts.Add(p);
                    }
                }
                
            }
            ViewBag.hotpro = Product_DAL.getListByIDList(HotProducts.getIDList());
            ViewBag.categoryLists = Category_DAL.getAllName();
            ViewBag.categoryList = Category_DAL.getAll();

            return View();
        }


        //根据类别列出商品
        //每个商品包含信息：图片、商品名称、价格
        public ActionResult ListByCla(string id)
        {
            if (User.Identity.Name != "")
            {
                ViewBag.state = 1;
            }
            else
            {
                ViewBag.state = 0;
            }

            ViewBag.productList = Product_DAL.getListByCategoryID(Convert.ToDecimal(id));

            ViewBag.categoryList = Category_DAL.getAll();
            ViewBag.category = id;

            return View();
        }


        //显示商品具体信息和评论信息
        //商品信息包括：商品名称(name)、商品价格(price)、商品描述(description)、库存(inventory)、类别(category_name)、预览图(url)
        //评论信息包括用户名称(name)、评价内容(content)、评价时间(date)
        //页面要有添加评论按钮
        public ActionResult Show(string id)
        {
            if (User.Identity.Name != "")
            {
                ViewBag.state = 1;
            }
            else
            {
                ViewBag.state = 0;
            }
            var pro = HotProducts.hotProducts.
                Where(p => p.productID == Convert.ToDecimal(id)).ToList();
            if (pro.Count==0)
            {
                HotProducts.hotProducts.Add(new HotProduct(Convert.ToDecimal(id)));
            }
            else
            {
                pro[0].clickNum++;
            }

            var Product = Product_DAL.getByID(Convert.ToDecimal(id));
            var listPro = new List<Product>();
            listPro.Add(Product);
            if (User.Identity.Name != "")
                Product_DAL.checkFavo(listPro, Convert.ToDecimal(User.Identity.Name));

            ViewBag.Product = Product_DAL.getByID(Convert.ToDecimal(id));
            ViewBag.categoryList = Category_DAL.getAll();
            return View();
        }

        //搜索商品信息
        //搜索商品名称含有搜素关键字的商品
        //列出商品
        //每个商品包含信息：图片、商品名称、价格
        public ActionResult Search()
        {
            if (User.Identity.Name != "")
            {
                ViewBag.state = 1;
            }
            else
            {
                ViewBag.state = 0;
            }

            var id = Request.Form["search_string"];
            ViewBag.productList = Product_DAL.getListBySearch(id);
            ViewBag.categoryList = Category_DAL.getAll();

            return View();
        }

  

    }
}