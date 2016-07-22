using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Activity_Product
    {
        public Activity_Product()
        {

        }

        public Activity_Product(decimal activityID, decimal productID)
        {
            ActivityID = activityID;
            ProductID = productID;
        }

        public Activity_Product(TP_ACTIVITY_PRODUCT ap)
        {
            ID = ap.ID;
            //Activity = new Activity(DBConn.db.TP_ACTIVITY.Find(ap.ACTIVITY_ID));
            ActivityID = ap.ACTIVITY_ID;
            //Product = new Product(DBConn.db.TP_PRODUCT.Find(ap.PRODUCT_ID));
            ProductID = ap.PRODUCT_ID;
        }

        public TP_ACTIVITY_PRODUCT CreateModel()
        {
            TP_ACTIVITY_PRODUCT tp = new TP_ACTIVITY_PRODUCT();
            try
            {
                tp.ACTIVITY_ID = ActivityID;
                tp.PRODUCT_ID = ProductID;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        //public Activity Activity { get; set; }
        public decimal ActivityID { get; set; }

        //public Product Product { get; set; }
        public decimal ProductID { get; set; }
        public decimal ID { get; set; }
    }
}