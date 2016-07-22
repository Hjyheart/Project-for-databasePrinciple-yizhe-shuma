using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_View_Model;


namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class Activity_Product_DAL
    {
        //Select
        public static Activity_Product getByID(decimal id)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                return new Activity_Product(db.TP_ACTIVITY_PRODUCT.Find(id));
            }
            catch
            {
                return null;
            }
        }

        public static List<Activity_Product> getAll()
        {
            OracleDbContext db = DBConn.createDbContext();
            var tpActivityProductList = db.TP_ACTIVITY_PRODUCT.ToList();
            return getListFromDBList(tpActivityProductList);
        }

        public static List<Activity_Product> getByActivityId(decimal id)
        {
            OracleDbContext db = DBConn.createDbContext();
            var tpList = (from a in db.TP_ACTIVITY_PRODUCT
                          where a.ACTIVITY_ID == id
                          select a).ToList();

            return getListFromDBList(tpList);
        }

        public static List<Activity_Product> getByProductId(decimal id)
        {
            OracleDbContext db = DBConn.createDbContext();
            var tpList = (from a in db.TP_ACTIVITY_PRODUCT
                          where a.PRODUCT_ID == id
                          select a).ToList();

            return getListFromDBList(tpList);
        }

        private static List<Activity_Product> getListFromDBList(List<TP_ACTIVITY_PRODUCT> tpActivityProductList)
        {
            var activityProductList = new List<Activity_Product>();

            foreach (TP_ACTIVITY_PRODUCT ap in tpActivityProductList)
            {
                activityProductList.Add(new Activity_Product(ap));
            }

            return activityProductList;
        }

        //Insert
        public static bool Insert(Activity_Product ap)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                var newActivityProduct = ap.CreateModel();
                db.TP_ACTIVITY_PRODUCT.Add(newActivityProduct);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        ////Update
        //public static bool Update(Activity_Product ap)
        //{
        //    try
        //    {
        //        var tpActivityProduct = DBConn.db.TP_ACTIVITY_PRODUCT.Find(ap.ID);
        //        tpActivityProduct.ACTIVITY_ID = ap.ActivityID;
        //        tpActivityProduct.PRODUCT_ID = ap.ProductID;

        //        var tpActivityProducts = getByActivityID(ap.ActivityID);
                

        //        DBConn.db.SaveChanges();
        //        return true;
        //    }
        //    catch
        //    {
        //    return false;
        //    }

        //}

        //Delete
        public static bool Delete(decimal id)
        {

            return false;
        }

        //Delete
        public static bool DeleteByID(decimal id)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                var tpActivityProduct = db.TP_ACTIVITY_PRODUCT.Find(id);
                db.TP_ACTIVITY_PRODUCT.Remove(tpActivityProduct);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteListByActivityID(decimal activityID)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                var tpActivityProducts = (from tpActivityProduct in db.TP_ACTIVITY_PRODUCT
                                          where tpActivityProduct.ACTIVITY_ID == activityID
                                          select tpActivityProduct).ToList();
                foreach (var tpActivityProduct in tpActivityProducts)
                {
                    db.TP_ACTIVITY_PRODUCT.Remove(tpActivityProduct);
                }
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static List<TP_ACTIVITY_PRODUCT> getListByActivityID(decimal act_id, int pageIndex, int pageSize)
        {
            try
            {
                int startpos = (pageIndex - 1) * pageSize;
                var db = DBConn.createDbContext();
                List<TP_ACTIVITY_PRODUCT> res = db.TP_ACTIVITY_PRODUCT.Where(item => item.ACTIVITY_ID == act_id).Skip(startpos).Take(pageSize).ToList();
                return res;
            }
            catch
            {
                return null;
            }
        }
    }
}