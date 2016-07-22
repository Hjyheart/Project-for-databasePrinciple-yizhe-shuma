using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Product
    {
        public Product()
        {
            IsFavo = false;
        }

        public Product(TP_PRODUCT tp)
        {
            ID = tp.ID;
            Name = tp.NAME;
            var db = DBConn.createDbContext();
            Category = new Category(db.TP_CATEGORY.Find(tp.CATEGORY_ID));
            Description = tp.DESCRIPTION;
            Price = tp.PRICE;
            AddDate = tp.ADD_DATE;
            Inventory = tp.INVENTORY;
            Details = tp.DETAILS;
            IsFavo = false;
        }

        public TP_PRODUCT CreateModel()
        {
            TP_PRODUCT tp = new TP_PRODUCT();
            try
            {
                tp.CATEGORY_ID = Category.ID;
                tp.NAME = Name;
                tp.DESCRIPTION = Description;
                tp.PRICE = Price;
                tp.DETAILS = Details;
                tp.ADD_DATE = AddDate;
                tp.INVENTORY = Inventory;
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

        public decimal Price { get; set; }

        public bool IsFavo { get; set; }

        public DateTime AddDate { get; set; }

        public decimal Inventory { get; set; }

        public Category Category { get; set; }

        public string Details { get; set; }

        //public string CategoryID { get; set; }

        //public List<Activity_Product> ActivityProductList { get; set; }

        public List<Comment> CommentList { get; set; }

        //public List<Exchange> Exchange { get; set; }

        //public List<Favorite> Favorite { get; set; }

        //public List<Order_Product> OrderProductList { get; set; }

        //public List<Return> RetuenList { get; set; }

        //public List<ShoppingCart> ShoppingCartList { get; set; }

        public List<Product_Image> ProductImageList { get; set; }

        public bool setProductImageList()
        {
            try
            {
   
                if (this.ProductImageList == null)
                    this.ProductImageList = new List<Product_Image>();

                var db = DBConn.createDbContext();
                var tpPIs = (from pi in db.TP_PRODUCT_IMAGE
                             where pi.PRODUCT_ID == ID
                             select pi).ToList();
                foreach (var PI in tpPIs)
                {
                    ProductImageList.Add(new Product_Image(PI));
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool setCommentList()
        {
            try
            {
                CommentList = new List<Comment>();
                var db = DBConn.createDbContext();
                var comID = (from com in db.TP_COMMENT
                             where com.PRODUCT_ID == ID
                             select com).ToList();
                foreach (var com in comID)
                {
                    CommentList.Add(new Comment(com));
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