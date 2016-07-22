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
    public class ManageActivityController : Controller
    {
        //列出活动
        //GET /ManageActivity?page=1
        public ActionResult Index(int page = 1, string query = null)
        {
            int pageSize = 20;
            int totalPages = 0;
            var activities = Activity_DAL.getPageList(page, pageSize, query, ref totalPages);
            ViewBag.currentPage = page;
            //ViewBag.totalPages = Activity_DAL.getTotalPage(page,pageSize,query);
            ViewBag.totalPages = totalPages;
            ViewBag.activityList = activities;
            return View();
        }

        //查看活动
        //GET /ManageActivity/Detail?id=002301
        public ActionResult Detail(int id)
        {
            var activity = Activity_DAL.getByID((decimal)id);
            ViewBag.activity = activity;
            ViewBag.productList = Activity_DAL.getProductListByID((decimal)id);
            ViewBag.imageList = Activity_DAL.getImageListByID((decimal)id);
            return View();
        }

        //添加活动
        //GET /ManageActivity/add
        public ActionResult Add()
        {
            return View();
        }

        //POST /ManageActivity/add
        [HttpPost]
        public string ConfirmAdd()
        {
            var productIDsTmp = Request.Form["productIDs"];
            string[] productIDs = null;
            if (productIDsTmp != null)
                productIDs = productIDsTmp.Split(',');
            var imageIDsTmp = Request.Form["imageIDs"];
            string[] imageIDs = null;
            if (imageIDsTmp != null)
                imageIDs = imageIDsTmp.Split(',');
            Activity newActivity = new Activity();
            newActivity.Name = Request.Form["name"];
            newActivity.Description = Request.Form["description"];
            newActivity.StartTime = Convert.ToDateTime(Request.Form["startTime"]);
            newActivity.EndTime = Convert.ToDateTime(Request.Form["endTime"]);
            newActivity.Discount = Decimal.Parse(Request.Form["discount"]);
            decimal id = Activity_DAL.Insert(newActivity);
            if (id == -1)//插入失败
                return JsonConvert.SerializeObject(new ReturnInformation("500", "插入活动失败", ""));
            newActivity.ID = id;

            //处理商品活动
            Activity_Product_DAL.DeleteListByActivityID(id);
            if (productIDs != null)
            {
                foreach (string productID in productIDs)
                {
                    Activity_Product ap = new Activity_Product(id, Decimal.Parse(productID));
                    if (!Activity_Product_DAL.Insert(ap))//修改活动对应的商品失败
                        return JsonConvert.SerializeObject(new ReturnInformation("500", "插入活动商品失败", ""));
                }
            }

            //处理商品图片
            if (imageIDs != null)
            {
                foreach (string imageID in imageIDs)
                {
                    Image image = Image_DAL.getByID(Decimal.Parse(imageID));
                    Activity_Image ai = new Activity_Image(id, image);
                    if (!Activity_Image_DAL.Insert(ai))//修改活动对应的商品失败
                        return null;
                }
            }

            string jsonTmp = JsonConvert.SerializeObject(newActivity);
            return JsonConvert.SerializeObject(new ReturnInformation("200", "", jsonTmp));
        }


        //编辑活动
        //GET /ManageActivity/edit?id=002301
        public ActionResult Edit(int id)
        {
            Activity activity = Activity_DAL.getByID((decimal)id);
            ViewBag.activity = activity;
            ViewBag.imageList = Activity_DAL.getImageListByID((decimal)id);
            return View();
        }

        public string GetActivityJson(int id)
        {
            Activity activity = Activity_DAL.getByID((decimal)id);
            ViewBag.activity = activity;
            ViewBag.imageList = Activity_DAL.getImageListByID((decimal)id);
            return JsonConvert.SerializeObject(new ReturnInformation("200", "", ViewBag));
        }

        //POST /ManageActivity/Confirm?id=002301
        [HttpPost]
        public string ConfirmEdit(int id)
        {
            var productIDsTmp = Request.Form["productIDs"];
            string[] productIDs = null;
            if (productIDsTmp != null)
                productIDs = productIDsTmp.Split(',');

            var imageIDsTmp = Request.Form["imageIDs"];
            string[] imageIDs = null;
            if (imageIDsTmp != null)
                imageIDs = imageIDsTmp.Split(',');

            Activity activity = new Activity();
            activity.ID = (decimal)id;
            activity.Name = Request.Form["name"];
            activity.Description = Request.Form["description"];
            activity.StartTime = Convert.ToDateTime(Request.Form["startTime"]);
            activity.EndTime = Convert.ToDateTime(Request.Form["endTime"]);
            activity.Discount = Decimal.Parse(Request.Form["discount"]);

            if (!Activity_DAL.Update(activity))//更新活动失败
                return JsonConvert.SerializeObject(new ReturnInformation("500", "活动更新失败", ""));
            Activity_Product_DAL.DeleteListByActivityID((decimal)id);
            if (productIDs != null)
            {
                foreach (string productID in productIDs)
                {
                    Activity_Product ap = new Activity_Product((decimal)id, Decimal.Parse(productID));
                    if (!Activity_Product_DAL.Insert(ap))
                        return JsonConvert.SerializeObject(new ReturnInformation("500", "活动商品更新失败", ""));
                }
            }

            Activity_Image_DAL.DeleteListByActivityID(id);
            if (imageIDs != null)
            {
                foreach (string imageID in imageIDs)
                {
                    Image image = Image_DAL.getByID(Decimal.Parse(imageID));
                    Activity_Image ai = new Activity_Image(id, image);
                    if (!Activity_Image_DAL.Insert(ai))
                        return JsonConvert.SerializeObject(new ReturnInformation("500", "活动图片更新失败", ""));
                }
            }
            return JsonConvert.SerializeObject(new ReturnInformation("200", "", JsonConvert.SerializeObject(activity)));
        }

        //删除活动(单个）
        //GET /ManageActivity/delete?id=002301
        public string Delete(int id)
        {
            if (Activity_DAL.Delete((decimal)id))
                return JsonConvert.SerializeObject(new ReturnInformation("200", "删除成功", ""));
            return JsonConvert.SerializeObject(new ReturnInformation("500", "删除失败", ""));

        }
    }
}
