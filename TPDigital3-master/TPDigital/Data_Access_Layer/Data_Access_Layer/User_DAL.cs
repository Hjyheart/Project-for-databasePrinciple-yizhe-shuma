using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Transactions;
using System.Web;
using TPDigital.Data_Access_Layer.Data_View_Model;
using TPDigital.Models;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class User_DAL
    {
        //Select
        public static User getByID(decimal id)
        {
            var db = DBConn.createDbContext();
            var result = db.TP_USER.Find(id);
            if (result == null)
                return null;
            else
                return new User(result);
        }

        public static TP_USER getTpUserByID(decimal id)
        {
            var db = DBConn.createDbContext();
            var result = db.TP_USER.Find(id);
            return result;
        }

        public static User getByEmail(string email)
        {
            var db = DBConn.createDbContext();
            List<TP_USER> result = (from user in db.TP_USER
                                    where email == user.EMAIL
                                    select user).ToList();
            if (result == null || result.Count == 0)
                return null;
            else
                return new User(result[0]);
        }
        public static User getByPhonenum(string phone_num)
        {
            var db = DBConn.createDbContext();
            List<TP_USER> result = (from user in db.TP_USER
                                    where phone_num == user.PHONE_NUMBER
                                    select user).ToList();
            if (result == null || result.Count == 0)
                return null;
            else
                return new User(result[0]);
        }
        public static bool Update(TP_USER item)
        {
            try
            {
                var db = DBConn.createDbContext();
                db.TP_USER.Attach(item);
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static List<User> getAll()
        {
            return null;
        }

        public static List<User> getListByName(String name)
        {
            return null;
        }

        //Insert
        public static int Insert(User user)
        {
            if (user.Email == null || user.Password == null || user.Name == null || user.PhoneNumber == null)
                return 2;//有空值
            var userGetByEmail = User_DAL.getByEmail(user.Email);
            if (userGetByEmail != null)
                return 3;//email存在
            var userGetByPhone = User_DAL.getByPhonenum(user.PhoneNumber);
            if (userGetByPhone != null)
                return 4;//phone 存在

            try
            {
                var tpUser = user.CreateModel();
                var db = DBConn.createDbContext();
                using (var ts = new TransactionScope())
                {
                    db.TP_USER.Add(tpUser);
                    db.Entry(tpUser).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();

                    User_Role_DAL.Insert(tpUser.ID, user.RoleList, false);
                    db.SaveChanges();

                    ts.Complete();
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //Update
        public static bool Update(User user)
        {
            return false;
        }

        //Delete
        public static bool Delete(long id)
        {
            return false;
        }

        public static List<User> getPageList(int pageIndex, int pageSize, string query, ref int totalPages)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                int startRow = (pageIndex - 1) * pageSize;
                var userList = new List<User>();
                List<TP_USER> tpUserList = new List<TP_USER>();
                IOrderedQueryable<TP_USER> tmp = null;
                if (query == null)
                    tmp = db.TP_USER.OrderBy(item => item.ID);
                else
                    tmp = db.TP_USER.Where(item => item.NAME.Contains(query) || item.EMAIL.Contains(query) || item.PHONE_NUMBER.Contains(query)).OrderBy(item=>item.ID);
                tpUserList = tmp.Skip(startRow).Take(pageSize).ToList();
                if(tmp!=null)
                    totalPages = (tmp.ToList().Count + pageSize - 1) / pageSize;
                foreach (TP_USER tpUser in tpUserList)
                {
                    userList.Add(new User(tpUser));
                }
                return userList;
            }
            catch
            {
                return null;
            }
        }

        public static List<Role> getRoleList(decimal UserID)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                List<Role> roles = new List<Role>();
                var tpUserRoles = (from tpUserRole in db.TP_USER_ROLE
                                   where tpUserRole.USER_ID == UserID
                                   select tpUserRole).ToList();
                foreach (var userRole in tpUserRoles)
                    roles.Add(Role_DAL.getByID(userRole.ROLE_ID));
                return roles;
            }
            catch
            {
                return null;
            }
        }
    }
}