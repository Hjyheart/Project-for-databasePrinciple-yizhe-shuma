using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Data_Access_Layer.Data_View_Model;
using TPDigital.Models;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class User_Role_DAL
    {
        public static bool Insert(decimal ID, List<Role> roleList, bool insertIntoDB=true)
        {
            try
            {
                var db = DBConn.createDbContext();
                foreach (Role role in roleList)
                {
                    var userRole = new TP_USER_ROLE();
                    userRole.USER_ID = ID;
                    userRole.ROLE_ID = (decimal)role.ID;
                    db.TP_USER_ROLE.Add(userRole);
                    db.Entry(userRole).State = System.Data.Entity.EntityState.Added;
                }
                if (insertIntoDB)
                    db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteByUserID(decimal userID)
        {
            try
            {
                var db = DBConn.createDbContext();
                var tpUserRoles = (from tpUserRole in db.TP_USER_ROLE
                                   where tpUserRole.USER_ID == userID
                                   select tpUserRole).ToList();
                foreach (var tpUserRole in tpUserRoles)
                {
                    db.TP_USER_ROLE.Remove(tpUserRole);
                }
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;


        }
    }
}