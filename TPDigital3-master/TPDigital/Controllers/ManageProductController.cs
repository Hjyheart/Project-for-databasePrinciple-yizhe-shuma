using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPDigital.Data_Access_Layer.Data_Access_Layer;
using TPDigital.Data_Access_Layer.Data_View_Model;

namespace TPDigital.Controllers
{
    [MyAuthorizeAttribute(Roles = "管理员")]
    public class ManageProductController : Controller
    {
        //列出商品
        //GET /ManageProduct?page=1
        public ActionResult Index(int page = 1,int category = 0,string query=null)
        {
            int pageSize = 20;
            int totalPages=0;
            var products = Product_DAL.getPageList(page, pageSize,(decimal)category,query,ref totalPages);
            ViewBag.products = products;
            ViewBag.currentPage = page;
            ViewBag.totalPages = totalPages;
            return View();
        }

        public string Json(int page = 1, int category = 0, string query = null)
        {
            int pageSize = 20;
            int totalPages = 0;
            var products = Product_DAL.getPageList(page, pageSize, (decimal)category, query, ref totalPages);
            ReturnJson returnJson = new ReturnJson(page.ToString(), totalPages.ToString(), JsonConvert.SerializeObject(products));
            return JsonConvert.SerializeObject(returnJson);
        }

        //查看商品
        //GET /ManageProduct/view?id=002301
        public ActionResult Detail(int id)
        {
            var product = Product_DAL.getByMyID((decimal)id);
            ViewBag.product = product;
            ViewBag.imageList = Product_DAL.getImageListByID((decimal)id);
            ViewBag.categoryList = Category_DAL.getAll();
            return View();
        }

        //添加商品
        //GET /ManageProduct/add
        public ActionResult Add()
        {
            ViewBag.categoryList = Category_DAL.getAll();
            return View();
        }

        //POST /ManageProduct/add
        [HttpPost]
        public string ConfirmAdd()
        {
            var imageIDsTmp = Request.Form["imageIDs"];
            string[] imageIDs = null;
            if (imageIDsTmp != null)
                imageIDs = imageIDsTmp.Split(',');
            Product newProduct = new Product();
            newProduct.Name = Request.Form["name"];
            newProduct.Description = Request.Form["description"];
            //类别未写
            newProduct.Price = Decimal.Parse(Request.Form["price"]);
            newProduct.AddDate = DateTime.Now;
            newProduct.Inventory = Decimal.Parse(Request.Form["inventory"]);
            newProduct.Category = Category_DAL.getByID(Decimal.Parse(Request.Form["category"]));
            newProduct.Details = Request.Form["details"];
            decimal id = Product_DAL.Insert(newProduct);
            if (id == -1)//插入失败
                return JsonConvert.SerializeObject(new ReturnInformation("500", "插入商品失败", ""));
            newProduct.ID = id;

            //处理商品图片
            if (imageIDs != null)
            {
                foreach (string imageID in imageIDs)
                {
                    Image image = Image_DAL.getByID(Decimal.Parse(imageID));
                    Product_Image pi = new Product_Image(id, image);
                    if (Product_Image_DAL.Insert(pi)==-1)//修改活动对应的商品失败
                        return JsonConvert.SerializeObject(new ReturnInformation("500", "活动商品更新失败", ""));
                }
            }
            return JsonConvert.SerializeObject(new ReturnInformation("200", "", JsonConvert.SerializeObject(newProduct)));
        }


        //编辑商品
        //GET /ManageProduct/edit?id=002301
        public ActionResult Edit(int id)
        {
            Product product = Product_DAL.getByMyID((decimal)id);
            var categorys = Category_DAL.getAll();
            ViewBag.product = product;
            ViewBag.categoryList = categorys;
            return View();
        }

        //GET
        public string GetImageList(int id)
        {
            var imageList = Product_DAL.getImageListByID((decimal)id);
            return JsonConvert.SerializeObject(imageList);
        }

        //POST /ManageProduct/edit?id=002301
        [HttpPost]
        public string ConfirmEdit(int id)
        {
            var imageIDsTmp = Request.Form["images"];
            string[] imageIDs = null;
            if (imageIDsTmp != null)
                imageIDs = imageIDsTmp.Split(',');
            Product product = new Product();
            product.ID = (decimal)id;
            product.Name = Request.Form["name"];
            product.Description = Request.Form["description"];
            product.Price = Decimal.Parse(Request.Form["price"]);
            product.Inventory = Decimal.Parse(Request.Form["inventory"]);
            product.Details = Request.Form["details"];
            product.Category = Category_DAL.getByID(Decimal.Parse(Request.Form["category"]));

            if (!Product_DAL.Update(product))//更新活动失败
                return JsonConvert.SerializeObject(new ReturnInformation("500", "商品更新失败", ""));

            //更新商品图片
            Product_Image_DAL.DeleteByProductID((decimal)id);
            if (imageIDs != null)
            {
                foreach (string imageID in imageIDs)
                {
                    Image image = Image_DAL.getByID(Decimal.Parse(imageID));
                    Product_Image pi = new Product_Image(id, image);
                    if (Product_Image_DAL.Insert(pi)==-1)
                        return JsonConvert.SerializeObject(new ReturnInformation("500", "商品图片更新失败", ""));
                }
            }
            return JsonConvert.SerializeObject(new ReturnInformation("200", "", JsonConvert.SerializeObject(product)));
        }

    }
}
