using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_View_Model;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class Image_DAL
    {
        public static string getTotalQuantity()
        {
            try
            {
                var db = DBConn.createDbContext();
                var quantity = (from tpImage in db.TP_IMAGE
                                select tpImage).Count();
                return quantity.ToString();
            }
            catch
            {
                return null;
            }
        }

        public static Image getByID(decimal? imageID)
        {
            try
            {
                var db = DBConn.createDbContext();
                TP_IMAGE tpImage = db.TP_IMAGE.Find(imageID);

                return new Image(tpImage);
            }
            catch
            {
                return null;
            }
        }

        public static decimal InsertByUrl(string url)
        {
            var tp = new TP_IMAGE();
            try
            {
                var db = DBConn.createDbContext();
                tp.URL = url;
                db.TP_IMAGE.Add(tp);
                db.SaveChanges();
            }
            catch
            {
                return -1;
            }
            return tp.ID;
        }

        public static decimal Insert(Image image)
        {
            var tpImage = image.CreateModel();
            try
            {
                var db = DBConn.createDbContext();
                db.TP_IMAGE.Add(tpImage);
                db.SaveChanges();
            }
            catch
            {
                return -1;
            }
            return tpImage.ID;
        }

        public static bool Update(Image image)
        {
            var db = DBConn.createDbContext();
            var needImage = db.TP_IMAGE.Find(image.ID);
            try
            {
                needImage.URL = image.URL;
                db.SaveChanges();    
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool Delete(decimal? id)
        {
            try
            {
                var db = DBConn.createDbContext();
                var image = db.TP_IMAGE.Find(id);
                db.TP_IMAGE.Remove(image);
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