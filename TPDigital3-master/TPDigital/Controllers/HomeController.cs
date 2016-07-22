using Qiniu.RS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPDigital.Data_Access_Layer.Data_View_Model;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Controllers
{

    public class HomeController : Controller
    {
        //[MyAuthorizeAttribute(Roles="管理员,顾客")]
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
            if (!HotProducts.inital)
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

            var hotpro = Product_DAL.getListByIDList(HotProducts.getIDList());
            ViewBag.hotpro = hotpro;
            ViewBag.categoryList = Category_DAL.getAll();
            ViewBag.normalpro = Product_DAL.getListBySize(30);

            var activities = Activity_DAL.getCurrentActivityList(4);

            List<Image> imageList = new List<Image>();
            foreach (var activity in activities)
            {
                var images = Activity_DAL.getImageListByID((decimal)activity.ID);
                if (images != null && images.Count > 0)
                {
                    imageList.Add(images[0]);
                }

            }
            ViewBag.Activity = activities;
            ViewBag.ActivityImage = imageList;
            return View();
        }

        public ActionResult NotFound()
        {
            if (User.Identity.Name != "")
            {
                ViewBag.state = 1;
            }
            else
            {
                ViewBag.state = 0;
            }
            ViewBag.categoryList = Category_DAL.getAll();
            return View();
        }
    }

}