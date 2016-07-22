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
    public class ManageOrderController : Controller
    {
        // GET /admin/manageorder?page=1
        public ActionResult Index(int page = 1, int state = -1)
        {
            int pageSize = 20;
            int totalPages = 0;
            var orders = Order_DAL.getPageList(page, pageSize,ref totalPages,state);
            ViewBag.currentPage = page;
            ViewBag.totalPages = totalPages;
            ViewBag.orderList = orders;
            return View();
        }

        //GET /admin/manageorder/detail?id=002301
        public ActionResult Detail(int id)
        {
            var order = Order_DAL.getByID((decimal)id);
            if (order == null)
                return View();
            order.addOrderProductList();
            return View(order);
        }

        public string Deliver(int id, int state)
        {
            var order = Order_DAL.getByID((decimal)id);
            if (Order_DAL.UpdateState((decimal)id, (decimal)state))
                return JsonConvert.SerializeObject(new ReturnInformation("200", "状态修改成功", ""));
            return JsonConvert.SerializeObject(new ReturnInformation("500", "状态修改失败", ""));
        }
    }
}