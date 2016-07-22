using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_View_Model;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class Product_Image_DAL
    {
        //Select
        public static Product_Image getByID(long id)
        {
            var db = DBConn.createDbContext();
            var tpProductImage = db.TP_PRODUCT_IMAGE.Find(id);
            var productImage = new Product_Image(tpProductImage);
            return productImage;
        }

        public static List<Product_Image> getByProductID(long id)
        {
            var db = DBConn.createDbContext();
            var tpProductImage = (from tpPI in db.TP_PRODUCT_IMAGE
                                  where tpPI.PRODUCT_ID == id
                                  select tpPI).ToList();
            var PIs = new List<Product_Image>();
            foreach(var tpPi in tpProductImage)
            {
                PIs.Add(new Product_Image(tpPi));
            }
            return PIs;
        }

        //Insert
        public static decimal Insert(Product_Image productImage)
        {
            var tpProductImage = productImage.CreateModel();
            try
            {
                var db = DBConn.createDbContext();
                tpProductImage.IMAGE_ID = Image_DAL.Insert(productImage.Image);
                db.TP_PRODUCT_IMAGE.Add(tpProductImage);
                //DBConn.db.SaveChanges();
            }
            catch
            {
                return -1;
            }
            return tpProductImage.ID;
        }

        //Update
        public static bool Update(Product_Image productImage)
        {
            try
            {
                var db = DBConn.createDbContext();
                var needProductImage = db.TP_PRODUCT_IMAGE.Find(productImage.ID);
                needProductImage.IMAGE_ID = productImage.Image.ID;
                needProductImage.PRODUCT_ID = productImage.ProductID;
                //Image_DAL.Update(productImage.Image);
                db.TP_PRODUCT_IMAGE.Attach(needProductImage);
                db.Entry(needProductImage).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Delete
        public static bool Delete(decimal id)
        {
            try
            {
                var db = DBConn.createDbContext();
                var productImage = db.TP_PRODUCT_IMAGE.Find(id);
                db.TP_PRODUCT_IMAGE.Remove(productImage);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;


        }

        public static bool DeleteByProductID(decimal id)
        {
            try
            {
                var db = DBConn.createDbContext();
                var tpProductImage = (from tpPI in db.TP_PRODUCT_IMAGE
                                      where tpPI.PRODUCT_ID == id
                                      select tpPI).ToList();
                foreach (var tpPi in tpProductImage)
                {
                    Image_DAL.Delete(tpPi.IMAGE_ID);
                    db.TP_PRODUCT_IMAGE.Remove(tpPi);
                }
                db.SaveChanges();

            }
            catch
            {
                return false;
            }
            return true;


        }

    }
}