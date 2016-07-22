using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_View_Model;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class Activity_DAL
    {
        public static TP_ACTIVITY getTPByID(decimal id)
        {
            try
            {
                var db = DBConn.createDbContext();
                TP_ACTIVITY res = db.TP_ACTIVITY.Find(id);
                return res;
            }
            catch
            {
                return null;
            }
        }

        public static List<TP_ACTIVITY> getListByEndTime(DateTime expire, int pageIndex, int pageSize)
        {
            try
            {
                int startpos = (pageIndex - 1) * pageSize;
                var db = DBConn.createDbContext();
                List<TP_ACTIVITY> res =
                    db.TP_ACTIVITY.Where(item => item.END_TIME > expire).Skip(startpos).Take(pageSize).ToList();
                return res;
            }
            catch
            {
                return null;
            }
        }


        public static Activity getByID(decimal id)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();

                TP_ACTIVITY tpActivity = db.TP_ACTIVITY.Find(id);

                return new Activity(tpActivity);
            }
            catch
            {
                return null;
            }
        }

        public static List<Activity> getAll()
        {
            var activityList = new List<Activity>();

            OracleDbContext db = DBConn.createDbContext();

            var tpActivityList = db.TP_ACTIVITY.ToList();

            foreach (TP_ACTIVITY tpActivity in tpActivityList)
            {
                activityList.Add(new Activity(tpActivity));
            }

            return activityList;
        }

        public static List<Activity> getPageList(int pageIndex, int pageSize, string query, ref int totalPages)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                int startRow = (pageIndex - 1) * pageSize;
                var activityList = new List<Activity>();
                List<TP_ACTIVITY> tpActivityList = new List<TP_ACTIVITY>();
                IOrderedQueryable<TP_ACTIVITY> tmp = null;
                if (query == null)
                    tmp = db.TP_ACTIVITY.OrderBy(item => item.ID);
                else
                    tmp = db.TP_ACTIVITY.Where(item => item.NAME.Contains(query)).OrderBy(item => item.ID);
                tpActivityList = tmp.Skip(startRow).Take(pageSize).ToList();
                if(tmp!=null)
                    totalPages = (tmp.ToList().Count + pageSize - 1) / pageSize;
                foreach (TP_ACTIVITY tpActivity in tpActivityList)
                {
                    activityList.Add(new Activity(tpActivity));
                }
                return activityList;
            }
            catch
            {
                return null;
            }
        }

        //public static int getTotalPage(int pageIndex, int pageSize, string query = null)
        //{
        //    try
        //    {
        //        int totalCnt = 0;
        //        if (query == null)
        //            totalCnt = DBConn.db.TP_ACTIVITY.ToList().Count;//待测试
        //        else
        //            totalCnt = DBConn.db.TP_ACTIVITY.Where(item => item.NAME.Contains(query)).ToList().Count;
        //        int totalPage = (totalCnt + pageSize - 1) / pageSize;
        //        return totalPage;
        //    }
        //    catch
        //    {
        //        return -1;
        //    }
        //}

        public static List<Activity> getListByName(String name)
        {
            var activityList = new List<Activity>();

            OracleDbContext db = DBConn.createDbContext();

            var tpActivityList = (from a in db.TP_ACTIVITY
                                  where a.NAME.Contains(name)
                                  select a).ToList();

            foreach (TP_ACTIVITY tpActivity in tpActivityList)
            {
                activityList.Add(new Activity(tpActivity));
            }

            return activityList;
        }

        public static List<Product> getProductListByID(decimal activityID)
        {
            try
            {
                var products = new List<Product>();

                OracleDbContext db = DBConn.createDbContext();

                var tpAcitivityProducts = (from acitivityProduct in db.TP_ACTIVITY_PRODUCT
                                           where acitivityProduct.ACTIVITY_ID == activityID
                                           select acitivityProduct).ToList();
                foreach (var acitivityProduct in tpAcitivityProducts)
                {
                    products.Add(Product_DAL.getByMyID(acitivityProduct.PRODUCT_ID));
                }
                return products;
            }
            catch
            {
                return null;
            }
        }

        public static List<Image> getImageListByID(decimal activityID)
        {
            try
            {
                var images = new List<Image>();

                OracleDbContext db = DBConn.createDbContext();

                var tpAcitivityImages = (from acitivityImage in db.TP_ACTIVITY_IMAGE
                                         where acitivityImage.ACTIVITY_ID == activityID
                                         select acitivityImage).ToList();
                foreach (var acitivityImage in tpAcitivityImages)
                {
                    images.Add(Image_DAL.getByID(acitivityImage.IMAGE_ID));
                }
                return images;
            }
            catch
            {
                return null;
            }
        }
        //获取当前时间活动列表显示给用户
        public static List<Activity> getCurrentActivityList(int cnt = -1)
        {
            var activityList = new List<Activity>();

            OracleDbContext db = DBConn.createDbContext();
            List<TP_ACTIVITY> tpActivityList = db.TP_ACTIVITY.Where(item => DateTime.Compare(item.START_TIME, DateTime.Now) < 0
                                          && DateTime.Compare(item.END_TIME, DateTime.Now) > 0).ToList(); ;
            if (cnt > 0 && tpActivityList.Count >= cnt)
            {
                tpActivityList = db.TP_ACTIVITY.Where(item => DateTime.Compare(item.START_TIME, DateTime.Now) < 0
                                          && DateTime.Compare(item.END_TIME, DateTime.Now) > 0).Take(cnt).ToList();
            }
            foreach (TP_ACTIVITY tpActivity in tpActivityList)
            {
                activityList.Add(new Activity(tpActivity));
            }
            return activityList;
        }

        //Insert
        public static decimal Insert(Activity activity)
        {
            try
            {
                var newActivity = activity.CreateModel();

                OracleDbContext db = DBConn.createDbContext();

                var tpActivity = db.TP_ACTIVITY.Add(newActivity);

                db.SaveChanges();

                return tpActivity.ID;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        //Update
        public static bool Update(Activity activity)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                var newActivity = db.TP_ACTIVITY.Find(activity.ID);
                newActivity.NAME = activity.Name;
                newActivity.DESCRIPTION = activity.Description;
                newActivity.DISCOUNT = activity.Discount;
                newActivity.START_TIME = activity.StartTime;
                newActivity.END_TIME = activity.EndTime;
                db.TP_ACTIVITY.Attach(newActivity);
                db.Entry(newActivity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        //Delete
        public static bool Delete(decimal id)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();

                var tpActivity = db.TP_ACTIVITY.Find(id);
                db.TP_ACTIVITY.Remove(tpActivity);
                //DBConn.db.Entry(tpActivity).State = System.Data.Entity.EntityState.Deleted;
                //    //删除对应的Activity_Product
                //    var tpActivityProducts = (from tpActivityProduct in DBConn.db.TP_ACTIVITY_PRODUCT
                //                              where tpActivityProduct.ACTIVITY_ID == id
                //                              select tpActivityProduct).ToList();
                //    foreach (var tpActivityProduct in tpActivityProducts)
                //    {
                //        DBConn.db.TP_ACTIVITY_PRODUCT.Remove(tpActivityProduct);
                //    }

                //    //删除对应的Activity_Image
                //    var tpActivityImages = (from tpActivityImage in DBConn.db.TP_ACTIVITY_PRODUCT
                //                              where tpActivityImage.ACTIVITY_ID == id
                //                              select tpActivityImage).ToList();
                //    foreach (var tpActivityImage in tpActivityImages)
                //    {
                //        DBConn.db.TP_ACTIVITY_PRODUCT.Remove(tpActivityImage);
                //    }
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