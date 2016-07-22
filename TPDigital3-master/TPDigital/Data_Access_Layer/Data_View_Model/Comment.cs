using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Comment
    {
        public Comment()
        {

        }
        
        public Comment(TP_COMMENT tp)
        {
            var db = DBConn.createDbContext();
            ID = tp.ID;
            UserID = tp.USER_ID;
            UserName = db.TP_USER.Find(UserID).NAME;
            ProductID = tp.PRODUCT_ID;
            OrderID = tp.ORDER_ID;
            Content = tp.CONTENT;
            AddDate = tp.ADD_DATE;
            Stars = tp.STARS;
            ID = tp.ID;
        }

        public TP_COMMENT CreateModel()
        {
            TP_COMMENT tp = new TP_COMMENT();
            try
            {
                tp.USER_ID = UserID;
                tp.PRODUCT_ID = ProductID;
                tp.ORDER_ID = OrderID;
                tp.CONTENT = Content;
                tp.ADD_DATE = AddDate;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        //public User User { get; set; }
        public decimal? UserID { get; set; }
        public string UserName { get; set; }
        public decimal? ProductID { get; set; }

        public decimal? OrderID { get; set; }
        public string Content { get; set; }

        public DateTime? AddDate { get; set; }             
        
        public decimal? Stars { get; set; }
        public decimal? ID { get; set; }
    }
}