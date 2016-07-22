using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class User_Role
    {
        public User_Role()
        {

        }

        public User_Role(TP_USER_ROLE tp)
        {
            ID = tp.ID;
            var db = DBConn.createDbContext();
            User = new User(db.TP_USER.Find(tp.USER_ID));
            Role = new Role(db.TP_ROLE.Find(tp.ROLE_ID));
        }

        public TP_USER_ROLE CreateModel()
        {
            TP_USER_ROLE tp = new TP_USER_ROLE();
            try
            {
                tp.USER_ID = (decimal)User.ID;
                tp.ROLE_ID = (decimal)Role.ID;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        public User User { get; set; }

        public Role Role { get; set; }

        public decimal ID { get; set; }  
    }
}