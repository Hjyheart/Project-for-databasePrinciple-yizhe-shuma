using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Data_Access_Layer.Data_View_Model;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class Role_DAL
    {
        public static Role getByName(string name)
        {
            var db = DBConn.createDbContext();
            var roleList = (from role in db.TP_ROLE
                            where role.NAME == name
                            select role).ToList();
            if (roleList.Count == 0)
                return null;
            return new Role(roleList[0]);
        }

        public static List<Role> getAll()
        {
            try
            {
                var db = DBConn.createDbContext();
                List<Role> roleList = new List<Role>();
                var tpRoleList = db.TP_ROLE.ToList();
                foreach (var tpRole in tpRoleList)
                    roleList.Add(new Role(tpRole));
                return roleList;
            }
            catch
            {
                return null;
            }
        }

        public static Role getByID(decimal id)
        {
            try
            {
                var db = DBConn.createDbContext();
                var roleList = db.TP_ROLE.Where(item => item.ID == id).ToList();
                if (roleList.Count == 0)
                    return null;
                return new Role(roleList[0]);
            }
            catch
            {
                return null;
            }
        }

    }
}