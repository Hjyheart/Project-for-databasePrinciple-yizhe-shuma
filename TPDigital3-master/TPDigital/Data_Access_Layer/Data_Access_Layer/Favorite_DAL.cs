using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;
namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class Favorite_DAL
    {
        public static List<TP_FAVORITE> getListByUserID(decimal userid, int pageIndex, int pageSize)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                int startpos = (pageIndex - 1) * pageSize;
                List<TP_FAVORITE> res = db.TP_FAVORITE.Where(item => item.USER_ID == userid).OrderBy(item => item.ID).Skip(startpos).Take(pageSize).ToList();
                return res;
            }
            catch
            {
                return null;
            }
        }

        public static decimal Insert(TP_FAVORITE item)
        {
            //try
            //{
            //    OracleDbContext db = DBConn.createDbContext();
            //    db.TP_FAVORITE.Add(item);
            //    db.Entry(item).State = System.Data.Entity.EntityState.Added;
            //    db.SaveChanges();
            //    return item.ID;
            //}
            //catch
            //{
            //    return -1;
            //}
            OracleDbContext db = DBConn.createDbContext();
            db.TP_FAVORITE.Add(item);
            db.Entry(item).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return item.ID;
        }

        public static bool Delete(decimal id)
        {
            OracleDbContext db = DBConn.createDbContext();
            TP_FAVORITE item = db.TP_FAVORITE.Find(id);
            if (item != null)
            {
                db.TP_FAVORITE.Remove(item);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool Update(TP_FAVORITE item)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                db.TP_FAVORITE.Attach(item);
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static int DeleteByUserAndProduct(decimal userid, decimal productid)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                List<TP_FAVORITE> res =
                    db.TP_FAVORITE.Where(item => item.USER_ID == userid && item.PRODUCT_ID == productid).ToList();
                if (res != null && res.Count() > 0)
                {
                    db.TP_FAVORITE.Remove(res[0]);
                    db.SaveChanges();
                    return 1;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}