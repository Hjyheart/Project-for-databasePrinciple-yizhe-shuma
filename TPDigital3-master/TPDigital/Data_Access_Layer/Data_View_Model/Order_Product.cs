using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Order_Product
    {
        public Order_Product()
        {

        }

        public Order_Product(TP_ORDER_PRODUCT tp)
        {
            ID = tp.ID;
            this.OrderID = tp.ORDER_ID;
            this.ProductID = tp.PRODUCT_ID;
            var db = DBConn.createDbContext();
            this.Product = new Product(db.TP_PRODUCT.Find(this.ProductID));
            Quantity = tp.QUANTITY;
            Price = tp.PRICE;
            if (tp.IS_RETURN == null)
                this.isReturn = false;
            else
                this.isReturn = (bool)tp.IS_RETURN;
        }

        public TP_ORDER_PRODUCT CreateModel()
        {
            TP_ORDER_PRODUCT tp = new TP_ORDER_PRODUCT();
            try
            {        
                tp.ORDER_ID = this.OrderID;
                tp.ID = ID;
                tp.PRODUCT_ID = this.ProductID;
                tp.QUANTITY = Quantity;
                tp.PRICE = Price;
                tp.IS_RETURN = isReturn;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        //public Order Order { get; set; }

        public bool setImageUrl()
        {
            if (Product == null)
                return false;
            Product.setProductImageList();
            if (Product.ProductImageList.Count == 0)
                return false;
            ImageUrl = Product.ProductImageList[0].Image.URL;
            return true;
        }

        public bool isReturn { get; set; }
        public string ImageUrl { get; set; }

        public Product Product { get; set; }
        public decimal OrderID { get; set; }
        public decimal ProductID { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal ID { get; set; }
    }
}