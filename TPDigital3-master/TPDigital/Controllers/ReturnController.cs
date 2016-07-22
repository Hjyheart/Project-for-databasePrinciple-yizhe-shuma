using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Controllers
{
    public class ReturnController : Controller
    {
        // GET: Return
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
            var returnList = Return_DAL.getListByUser(user);
            ViewBag.categoryList = Category_DAL.getAll();
            ViewBag.orderList = returnList;
            ViewBag.user = user;
            return View();
        }

        //把一个orderProduct的状态改为审核中
        //必须之前状态为已签收，orderProduct的isreturn为false
        [HttpPost]
        public int ApplyReturn(FormCollection fc, decimal orderProductID)
        {
            try
            {
                var tpOrderProduct = Order_Product_DAL.getTpByID(orderProductID);
                var product = Order_DAL.getTpOrderByID(tpOrderProduct.ORDER_ID);

                if (tpOrderProduct.IS_RETURN == true ||
                    product.PACKAGE_STATE_ID != State_DAL.getPackageStateByName("已签收").ID)
                    return 0;

                tpOrderProduct.IS_RETURN = true;
                if (!Order_Product_DAL.update(tpOrderProduct))
                    return 0;
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        [HttpPost]
        public int UpdateExpress(FormCollection fc, decimal returnID, string checkNumber)
        {
            try
            {
                var tpReturn = Return_DAL.getTpByID(returnID);
                if (tpReturn.PACKAGE_STATE != State_DAL.getPackageStateByName("待退回").ID)
                    return 0;
                var tpExpress = Express_DAL.getTpByID((decimal)tpReturn.EXPRESS_ID);
                tpExpress.CHECK_NUMBER = checkNumber;
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}