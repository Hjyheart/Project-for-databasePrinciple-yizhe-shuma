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
    class NewRole
    {
        public decimal ID { get; set; }

        public string Name { get; set; }

        public bool Belong { get; set; }

        public NewRole() { }
        public NewRole(decimal id, string name, bool belong)
        {
            ID = id;
            Name = name;
            Belong = belong;
        }
    }

    //[MyAuthorizeAttribute(Roles = "管理员")]
    public class ManageUserController : Controller
    {
        // GET: ManageUser
        public ActionResult Index(int page = 1, string query = null)
        {
            int pageSize = 20;
            int totalPages = 0;
            var users = User_DAL.getPageList(page, pageSize, query, ref totalPages);
            foreach (User user in users)
            {
                user.RoleList = User_DAL.getRoleList(user.ID);
            }
            ViewBag.currentPage = page;
            ViewBag.totalPages = totalPages;
            ViewBag.userList = users;
            return View();
        }

        public ActionResult Detail(int id)
        {
            var user = User_DAL.getByID((decimal)id);
            var roleList = User_DAL.getRoleList(user.ID);
            user.RoleList = User_DAL.getRoleList(user.ID);
            List<Role> allRole = Role_DAL.getAll();
            List<NewRole> newRoleList = new List<NewRole>();
            foreach (var role in roleList)
                newRoleList.Add(new NewRole(role.ID, role.Name, true));
            foreach (var role in allRole)
                if (newRoleList.Find(item => item.ID == role.ID) == null)
                    newRoleList.Add(new NewRole(role.ID, role.Name, false));
            ViewBag.user = user;
            ViewBag.roleList = newRoleList;
            ViewBag.id = id;
            return View();
        }

        //GET
        public ActionResult Edit(int id)
        {
            User user = User_DAL.getByID((decimal)id);
            ViewBag.user = user;
            return View();
        }

        [HttpPost]
        public string ConfirmEdit(int id)
        {
            var roleIDsTmp = Request.Form["roles"];
            string[] roleIDs = null;
            if (roleIDsTmp != null)
                roleIDs = roleIDsTmp.Split(',');
            User_Role_DAL.DeleteByUserID(id);
            List<Role> roleList = new List<Role>();
            if (roleIDs != null)
            {
                foreach (string roleID in roleIDs)
                {
                    Role role = Role_DAL.getByID(Decimal.Parse(roleID));
                    roleList.Add(role);
                }
                if(!User_Role_DAL.Insert((decimal)id, roleList))
                    return JsonConvert.SerializeObject(new ReturnInformation("500", "原角色已删除，新角色加入失败", ""));
            }
            return JsonConvert.SerializeObject(new ReturnInformation("200", "", ""));
        }


    }
}