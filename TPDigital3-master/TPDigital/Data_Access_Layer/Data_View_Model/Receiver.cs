using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Receiver
    {
        public Receiver()
        {

        }

        public Receiver(TP_RECEIVER tp)
        {
            ID = tp.ID;
            Name = tp.NAME;
            PhoneNumber = tp.PHONE_NUMBER;
            this.UserID = tp.USER_ID;
            var db = DBConn.createDbContext();
            Location = new Location(db.TP_LOCATION.Find(tp.LOCATION_ID));
            AddrDetail = tp.ADDR_DETAIL;
        }

        public TP_RECEIVER CreateModel()
        {
            TP_RECEIVER tp = new TP_RECEIVER();
            try
            {
                tp.NAME = Name;
                tp.PHONE_NUMBER = PhoneNumber;
                tp.USER_ID = this.UserID;
                tp.LOCATION_ID = Location.ID;
                tp.ADDR_DETAIL = AddrDetail;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        public decimal ID { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public decimal UserID { get; set; }

        public Location Location { get; set; }

        public string AddrDetail { get; set; }

        public List<Order> OrderList { get; set; }
    }
}