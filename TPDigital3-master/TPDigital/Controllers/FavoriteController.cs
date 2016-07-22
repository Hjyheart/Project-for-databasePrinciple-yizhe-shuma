using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPDigital.Controllers;
using TPDigital.Data_Access_Layer.Data_Access_Layer;
using TPDigital.Data_Access_Layer.Data_View_Model;
using TPDigital.Models;
namespace TPDigital.Controllers
{
    public class FavoriteController : Controller
    {
        [MyAuthorizeAttribute(Roles = "顾客")]
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
            decimal userid = decimal.Parse(User.Identity.Name);
            TP_USER user = User_DAL.getTpUserByID(userid);
            if (user != null)
            {
                List<Product> products = new List<Product>();
                List<TP_FAVORITE> favs = Favorite_DAL.getListByUserID(user.ID, 1, 20);
                if (favs != null)
                {
                    foreach (var fav in favs)
                    {
                        products.Add(Product_DAL.getByID(fav.PRODUCT_ID));
                    }
                }
                else favs = new List<TP_FAVORITE>();
                ViewBag.User = user;
                ViewBag.Favs = favs;
                ViewBag.Pros = products;
                ViewBag.categoryList = Category_DAL.getAll();

                return View("index");
            }
            //应该返回error
            return null;

        }
        //GET: /Favorite/AddFavorite/product_id
        [MyAuthorizeAttribute]
        public int AddFavorite(string id)
        {
            TP_FAVORITE tp_fav = new TP_FAVORITE();
            tp_fav.USER_ID = long.Parse(User.Identity.Name);
            tp_fav.PRODUCT_ID = long.Parse(id);
            return (int)Favorite_DAL.Insert(tp_fav);
        }
        //GET: /Favorite/DeleteFavorite/product_id
        [MyAuthorizeAttribute]
        public int DeleteFavorite(string id)
        {
            if(Favorite_DAL.Delete(decimal.Parse(id)))
            {
                return 1;
            }
            return 0;
        }
        [MyAuthorizeAttribute]
        public int DeleteFavoriteByProduct(string id)
        {
            return Favorite_DAL.DeleteByUserAndProduct(decimal.Parse(User.Identity.Name), decimal.Parse(id));
        }
    }
}
