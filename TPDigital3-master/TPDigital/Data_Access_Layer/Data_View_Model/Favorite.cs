using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Favorite
    {
        public Favorite()
        {

        }

        public Favorite(TP_FAVORITE tp)
        {
            ID = tp.ID;
            //User = new User(DBConn.db.TP_USER.Find(tp.USER_ID));
            UserID = tp.USER_ID;
            var db = DBConn.createDbContext();
            Product = new Product(db.TP_PRODUCT.Find(tp.PRODUCT_ID));
        }

        public TP_FAVORITE CreateModel()
        {
            TP_FAVORITE tp = new TP_FAVORITE();
            try
            {
                tp.USER_ID = UserID;
                tp.PRODUCT_ID = Product.ID;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        public decimal UserID { get; set; }

        public Product Product { get; set; }

        public decimal ID { get; set; }
    }
}