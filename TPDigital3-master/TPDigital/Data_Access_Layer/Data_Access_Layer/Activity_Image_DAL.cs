using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_View_Model;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class Activity_Image_DAL
    {
        //Select
        public static Activity_Image getByID(decimal id)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();

                var tpActivityImage = db.TP_ACTIVITY_IMAGE.Find(id);

                return new Activity_Image(tpActivityImage);
            }
            catch
            {
                return null;
            }
        }

        public static List<Activity_Image> getAll()
        {
            OracleDbContext db = DBConn.createDbContext();

            var tpActivityImageList = db.TP_ACTIVITY_IMAGE.ToList();

            return getListFromDBList(tpActivityImageList);
        }

        public static List<Activity_Image> getByActivityID(decimal id)
        {
            OracleDbContext db = DBConn.createDbContext();
            var tpActivityImageList = (from a in db.TP_ACTIVITY_IMAGE
                                        where a.ACTIVITY_ID == id
                                        select a).ToList();

            return getListFromDBList(tpActivityImageList);
        }

        public static List<Activity_Image> getByImageID(decimal id)
        {
            OracleDbContext db = DBConn.createDbContext();
            var tpActivityImageList = (from a in db.TP_ACTIVITY_IMAGE
                                       where a.IMAGE_ID == id
                                       select a).ToList();

            return getListFromDBList(tpActivityImageList);
        }

        private static List<Activity_Image> getListFromDBList(List<TP_ACTIVITY_IMAGE> tpActivityImageList)
        {
            var activityImageList = new List<Activity_Image>();

            foreach (TP_ACTIVITY_IMAGE ai in tpActivityImageList)
            {
                activityImageList.Add(new Activity_Image(ai));
            }

            return activityImageList;
        }


        

        //Insert
        public static bool Insert(Activity_Image ai)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                var newActivityImage = ai.CreateModel();
                db.TP_ACTIVITY_IMAGE.Add(newActivityImage);
                Image_DAL.Insert(ai.Image);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Update
        public static bool Update(Activity_Image ai)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                var newActivityImage = db.TP_ACTIVITY_IMAGE.Find((Decimal)ai.ID);
                newActivityImage.ID = ai.ID;
                newActivityImage.IMAGE_ID = ai.Image.ID;
                newActivityImage.ACTIVITY_ID = ai.ActivityID;
                db.TP_ACTIVITY_IMAGE.Attach(newActivityImage);
                db.Entry(newActivityImage).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Delete
        public static bool Delete(decimal id)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                db.TP_ACTIVITY_IMAGE.Remove(db.TP_ACTIVITY_IMAGE.Find(id));
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool DeleteListByActivityID(decimal activityID)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                var tpActivityImages = (from tpActivityImage in db.TP_ACTIVITY_IMAGE
                                        where tpActivityImage.ACTIVITY_ID == activityID
                                        select tpActivityImage).ToList();
                foreach (var tpActivityImage in tpActivityImages)
                {
                    db.TP_ACTIVITY_IMAGE.Remove(tpActivityImage);
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