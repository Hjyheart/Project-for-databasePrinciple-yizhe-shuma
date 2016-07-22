using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Activity_Image
    {
        public Activity_Image()
        {

        }

        public Activity_Image(decimal activityID, Image image)
        {
            ActivityID = activityID;
            Image = image;
        }

        public Activity_Image(TP_ACTIVITY_IMAGE tpActivityImage)
        {
            ID = tpActivityImage.ID;
            //Activity = new Activity(DBConn.db.TP_ACTIVITY.Find(tpActivityImage.ACTIVITY_ID));
            ActivityID = tpActivityImage.ACTIVITY_ID;

            var db = DBConn.createDbContext();
            Image = new Image(db.TP_IMAGE.Find(tpActivityImage.IMAGE_ID));
            //ImageID = tpActivityImage.IMAGE_ID;
            //ImageUrl = DBConn.db.TP_IMAGE.Find(ImageID).URL;
        }

        public TP_ACTIVITY_IMAGE CreateModel()
        {
            TP_ACTIVITY_IMAGE tp = new TP_ACTIVITY_IMAGE();
            try
            {
                tp.ACTIVITY_ID = ActivityID;
                tp.IMAGE_ID = Image.ID;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        public decimal? ActivityID { get; set; }

        public Image Image { get; set; }
        //public decimal? ImageID { get; set; }

        //public string ImageUrl { get; set; }
        public decimal ID { get; set; }
    }
}