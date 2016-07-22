using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Activity
    {
        public Activity()
        {

        }

        public Activity(TP_ACTIVITY tpActivity)
        {
            ID = tpActivity.ID;
            Name = tpActivity.NAME;
            Description = tpActivity.DESCRIPTION;
            Discount = tpActivity.DISCOUNT;
            StartTime = tpActivity.START_TIME;
            EndTime = tpActivity.END_TIME;
        }

        public TP_ACTIVITY CreateModel()
        {
            TP_ACTIVITY tp = new TP_ACTIVITY();
            try
            {
                tp.NAME = Name;
                tp.DESCRIPTION = Description;
                tp.DISCOUNT = Discount;
                tp.START_TIME = StartTime;
                tp.END_TIME = EndTime;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        public decimal ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public decimal Discount { get; set; }

        public List<Activity_Product> ActivityProductList { get; set; }

        public List<Activity_Image> ActivityImageList {get;set;}

        //public bool setActivityProductList()
        //{

        //    try
        //    {
        //        var tpPIs = (from pi in DBConn.db.TP_PRODUCT_IMAGE
        //                     where pi.PRODUCT_ID == ID
        //                     select pi).ToList();
        //        foreach (var PI in tpPIs)
        //        {
        //            ActivityProductList.Add(new Activity_Product(PI));
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        public bool setActivityImageList()
        {
            try
            {
                if (this.ActivityImageList == null)
                    this.ActivityImageList = new List<Activity_Image>();

                var db = DBConn.createDbContext();
                var tpAIs = (from tpAI in db.TP_ACTIVITY_IMAGE
                             where tpAI.ACTIVITY_ID == ID
                             select tpAI).ToList();
                foreach (var tpAI in tpAIs)
                {
                    ActivityImageList.Add(new Activity_Image(tpAI));
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}