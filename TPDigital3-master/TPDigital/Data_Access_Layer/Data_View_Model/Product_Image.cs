using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Product_Image
    {
        public Product_Image()
        {

        }

        public Product_Image(decimal productID, Image image)
        {
            ProductID = productID;
            Image = image;
        }

        public Product_Image(TP_PRODUCT_IMAGE tp)
        {
            ID = tp.ID;
            var db = DBConn.createDbContext();
            Image = new Image(db.TP_IMAGE.Find(tp.IMAGE_ID));
            ProductID = tp.ID;
        }

        public TP_PRODUCT_IMAGE CreateModel()
        {
            TP_PRODUCT_IMAGE tp = new TP_PRODUCT_IMAGE();
            try
            {
                tp.IMAGE_ID = Image.ID;
                tp.PRODUCT_ID = ProductID;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        public Image Image { get; set; }

       // public String ImageUrl { get; set; }

        public Decimal ProductID { get; set; }

        public decimal ID { get; set; }
    }
}