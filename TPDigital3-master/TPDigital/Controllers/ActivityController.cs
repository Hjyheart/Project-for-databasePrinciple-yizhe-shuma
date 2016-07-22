using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;
using TPDigital.Data_Access_Layer.Data_View_Model;

namespace TPDigital.Controllers
{
    public class ActivityController : Controller
    {
        // GET: Activity
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
            int cnt = 4;
            var activities = Activity_DAL.getCurrentActivityList(cnt);
            List<Image> imageList = new List<Image>();
            foreach (var activity in activities)
            {
                activity.setActivityImageList();
                //var images = Activity_DAL.getImageListByID((decimal)activity.ID);
                //if(images != null && images.Count > 0)
                //{
                //    imageList.Add(images[0]);
                //}               
            }
            ViewBag.activities = activities;
            //ViewBag.images = imageList;
            ViewBag.categoryList = Category_DAL.getAll();
            return View();
        }


        //活动详情
        //GET
        public ActionResult Detail(int id)
        {
            if (User.Identity.Name != "")
            {
                ViewBag.state = 1;
            }
            else
            {
                ViewBag.state = 0;
            }
            var activity = Activity_DAL.getByID((decimal)id);
            var images = Activity_DAL.getImageListByID((decimal)id);
            var products = Activity_DAL.getProductListByID((decimal)id);
            foreach (var product in products)
                product.setProductImageList();
            ViewBag.activity = activity;
            ViewBag.activityImages = images;
            ViewBag.products = products;
            ViewBag.categoryList = Category_DAL.getAll();
            return View();
        }
    }
}