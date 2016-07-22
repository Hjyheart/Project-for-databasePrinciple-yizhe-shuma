using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {

        }

        public ShoppingCart(TP_SHOPPING_CART tp)
        {
            ID = tp.ID;
            UserID = tp.USER_ID;
            var db = DBConn.createDbContext();
            Product = new Product(db.TP_PRODUCT.Find(tp.PRODUCT_ID));
            Quantity = (long)tp.QUANTITY;
        }

        public TP_SHOPPING_CART CreateModel()
        {
            TP_SHOPPING_CART tp = new TP_SHOPPING_CART();
            try
            {
                tp.USER_ID = UserID;
                tp.PRODUCT_ID = Product.ID;
                tp.QUANTITY = Quantity;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        public decimal UserID { get; set; }

        public Product Product { get; set; }

        public decimal Quantity { get; set; }

        public decimal ID { get; set; }
    }
}