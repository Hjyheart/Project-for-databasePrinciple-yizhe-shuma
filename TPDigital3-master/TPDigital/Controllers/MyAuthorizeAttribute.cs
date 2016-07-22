using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPDigital.Data_Access_Layer.Data_Access_Layer;
using TPDigital.Models;

namespace TPDigital.Controllers
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            var db = DBConn.createDbContext();
            if (httpContext.User != null)
            {
                if (httpContext.User.Identity.Name == "")
                    return false;
                var user = User_DAL.getByID(decimal.Parse(httpContext.User.Identity.Name));
                if (user == null)
                    return false;
                var roles = Roles.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (roles.Length == 0)
                    return true;

                return (from s in roles
                        from user_role in db.TP_USER_ROLE
                        from current_roles in db.TP_ROLE
                        where user.ID.Equals((long)user_role.USER_ID) && current_roles.ID.Equals(user_role.ROLE_ID) && s.Equals(current_roles.NAME)
                        select s).Any();
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //验证不通过,直接跳转到相应页面，注意：如果不使用以下跳转，则会继续执行Action方法
            filterContext.Result = new RedirectResult("~/User/LoginView",true);
        }
    }
}