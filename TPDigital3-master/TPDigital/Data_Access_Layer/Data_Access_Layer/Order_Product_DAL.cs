using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Data_Access_Layer.Data_View_Model;
using TPDigital.Models;



namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class Order_Product_DAL
    {
        // public decimal ORDER_ID { get; set; }

        //public decimal PRODUCT_ID { get; set; }

        //public decimal QUANTITY { get; set; }

        //public decimal PRICE { get; set; }
        public static bool Create(decimal orderID, Product product, decimal quantity)
        {
            if (product.Inventory < quantity)
                return false;
            TP_ORDER_PRODUCT orderProduct = new TP_ORDER_PRODUCT();
            
            orderProduct.ORDER_ID = orderID;
            orderProduct.PRICE = product.Price;
            orderProduct.QUANTITY = quantity;
            orderProduct.PRODUCT_ID = product.ID;

            try
            {
                OracleDbContext db = DBConn.createDbContext();
                db.TP_ORDER_PRODUCT.Add(orderProduct);
                db.Entry(orderProduct).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public static Order_Product getByID(decimal ID)
        {
            var db = DBConn.createDbContext();
            var item = db.TP_ORDER_PRODUCT.Find(ID);
            if (item == null)
                return null;
            else
                return new Order_Product(item);
        }

        public static TP_ORDER_PRODUCT getTpByID(decimal ID)
        {
            var db = DBConn.createDbContext();
            var item = db.TP_ORDER_PRODUCT.Find(ID);
            return item;
        }
        public static bool update(TP_ORDER_PRODUCT item)
        {
            try
            {
                var db = DBConn.createDbContext();
                db.TP_ORDER_PRODUCT.Attach(item);
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}