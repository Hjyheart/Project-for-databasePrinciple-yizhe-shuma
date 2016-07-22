using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Data_Access_Layer.Data_View_Model;
using TPDigital.Models;
using System.Transactions;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class Product_DAL
    {
        public static bool checkFavo(List<Product> listPro, decimal id)
        {
            try
            {
                var tpList = Favorite_DAL.getListByUserID(id, 1, 100);
                int i = 0;
                foreach (var pro in listPro)
                {
                    foreach(var tpPro in tpList)
                    {
                        if (pro.ID == tpPro.ID)
                        {
                            pro.IsFavo = true;
                        }
                    }
                   
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        

        //Select
        public static TP_PRODUCT getTpProductByID(decimal ID)
        {
            try
            {
                var db = DBConn.createDbContext();
                var item = db.TP_PRODUCT.Find(ID);
                return item;
            }
            catch
            {
                return null;
            }
        }

        public static Product getByID(decimal id)
        {
            try
            {
                var db = DBConn.createDbContext();
                var tpProduct = db.TP_PRODUCT.Find(id);
                if (tpProduct == null)
                    return null;
                var product = new Product(tpProduct);
                product.setProductImageList();
                product.setCommentList();
                return product;
            }
            catch
            {
                return null;
            }
        }

        public static Product getByMyID(decimal id)
        {
            try
            {
                var db = DBConn.createDbContext();
                var tpProduct = db.TP_PRODUCT.Find(id);
                if (tpProduct == null)
                    return null;
                var product = new Product(tpProduct);
                return product;
            }
            catch
            {
                return null;
            }
        }

        public static List<Product> getAll()
        {
            try
            {
                var db = DBConn.createDbContext();
                var products = db.TP_PRODUCT.ToList();
                var produList = new List<Product>();
                foreach (var p in products)
                {
                    var temp = new Product(p);
                    temp.setProductImageList();
                    //temp.setCommentList();
                    produList.Add(temp);
                }
                return produList;
            }
            catch
            {
                return null;
            }
        }


        public static List<Product> getListBySize(int size)
        {
            var produList = new List<Product>();
            try
            {
                var db = DBConn.createDbContext();
                var products = db.TP_PRODUCT.OrderBy(item => item.ID).Skip(0).Take(size).ToList();

                foreach (var p in products)
                {
                    var temp = new Product(p);
                    temp.setProductImageList();
                    //temp.setCommentList();
                    produList.Add(temp);
                }
            }
            catch
            {
                return null;
            }
            return produList;
        }

        public static List<Product> getListBySearch(String name)
        {
            var db = DBConn.createDbContext();
            var products = db.TP_PRODUCT.ToList();
            products = products.Where(w => w.NAME.Contains(name)||w.DESCRIPTION.Contains(name)).ToList();
            var produList = new List<Product>();
            foreach (var produ in products)
            {
                var pro = new Product(produ);
                pro.setProductImageList();
                produList.Add(pro);
            }
            return produList;

        }

        public static List<Product> getListByCategory(String name)
        {
            var pros = new List<Product>();
            try
            {
                var categoryid = Category_DAL.getByName(name).ID;
                var db = DBConn.createDbContext();
                var productList = (from pro in db.TP_PRODUCT
                                   where pro.CATEGORY_ID == categoryid
                                   select pro).ToList();

                foreach (var produ in productList)
                {
                    var pro = new Product(produ);
                    pro.setProductImageList();
                    pros.Add(pro);
                }
            }
            catch
            {
                return null;
            }
            return pros;
        }

        public static List<Product> getListByCategoryID(decimal id)
        {
            var pros = new List<Product>();
            try
            {
                var db = DBConn.createDbContext();
                var productList = (from pro in db.TP_PRODUCT
                                   where pro.CATEGORY_ID == id
                                   select pro);

                foreach (var produ in productList)
                {
                    var pro = new Product(produ);
                    pro.setProductImageList();
                    pros.Add(pro);
                }
            }
            catch
            {
                return null;
            }
            return pros;
        }

        //Insert
        public static decimal Insert(Product product)
        {
            var tpProduct = product.CreateModel();
            try
            {
                var db = DBConn.createDbContext();
                db.TP_PRODUCT.Add(tpProduct);

                //foreach(var pi in product.ProductImageList)  
                //{
                //    Product_Image_DAL.Insert(pi);
                //}
                db.SaveChanges();
            }
            catch
            {
                return -1;
            }
            return tpProduct.ID;
        }

        //Update
        public static bool Update(Product product)
        {
            try
            {
                var db = DBConn.createDbContext();
                var needProduct = db.TP_PRODUCT.Find((Decimal)product.ID);
                needProduct.NAME = product.Name;
                needProduct.PRICE = (Decimal)product.Price;
                needProduct.CATEGORY_ID = (Decimal)product.Category.ID;
                needProduct.DESCRIPTION = product.Description;
                needProduct.ADD_DATE = product.AddDate;
                needProduct.INVENTORY = product.Inventory;
                db.TP_PRODUCT.Attach(needProduct);
                db.Entry(needProduct).State = System.Data.Entity.EntityState.Modified;
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
                var Product = db.TP_PRODUCT.Find(id);
                Product_Image_DAL.DeleteByProductID((long)Product.ID);
                db.TP_PRODUCT.Remove(Product);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;

        }

        public static List<Product> getListByIDList(List<decimal> productIDList)
        {
            try
            {
                List<Product> productList = new List<Product>();
                foreach (int ID in productIDList)
                {
                    productList.Add(getByID(ID));
                }
                return productList;
            }
            catch
            {
                return null;
            }
        }

        public static List<TP_PRODUCT> getFavoriteListByUserID(decimal userid, int pageIndex, int pageSize)
        {
            try
            {
                int startpos = (pageIndex - 1) * pageSize;
                var db = DBConn.createDbContext();
                List<TP_PRODUCT> res = (from product in db.TP_PRODUCT
                                        from fav in db.TP_FAVORITE
                                        where product.ID == fav.PRODUCT_ID && fav.USER_ID == userid
                                        select product).OrderBy(item => item.ID).Skip(startpos).Take(pageSize).ToList();
                return res;
            }
            catch
            {
                return null;
            }
        }

        public static List<Image> getImageListByID(decimal productID)
        {
            try
            {
                var images = new List<Image>();
                var db = DBConn.createDbContext();
                var tpProductImages = (from tpProductImage in db.TP_PRODUCT_IMAGE
                                       where tpProductImage.PRODUCT_ID == productID
                                       select tpProductImage).ToList();
                foreach (var productImage in tpProductImages)
                    images.Add(Image_DAL.getByID(productImage.IMAGE_ID));
                return images;
            }
            catch
            {
                return null;
            }
        }

        public static List<Product> getPageList(int pageIndex, int pageSize, decimal category, string query, ref int totalPage)
        {
            
            try
            {
                var db = DBConn.createDbContext();
                int startRow = (pageIndex - 1) * pageSize;
                var productList = new List<Product>();
                List<TP_PRODUCT> tpProductList = new List<TP_PRODUCT>();
                IOrderedQueryable<TP_PRODUCT> tmp = null;
                if (category == 0)
                {
                    if (query == null)
                        tmp = db.TP_PRODUCT.OrderBy(item => item.ID);
                    else
                        tmp = db.TP_PRODUCT.Where(item => item.NAME.Contains(query)).OrderBy(item => item.ID);
                }
                else
                {
                    if (query == null)
                        tmp = db.TP_PRODUCT.Where(item => item.CATEGORY_ID == category).OrderBy(item => item.ID);
                    else
                        tmp = db.TP_PRODUCT.Where(item => item.CATEGORY_ID == category && item.NAME.Contains(query)).OrderBy(item => item.ID);
                }
                tpProductList = tmp.Skip(startRow).Take(pageSize).ToList();
                if(tmp!=null)
                    totalPage = (tmp.ToList().Count + pageSize - 1) / pageSize;
                foreach (TP_PRODUCT tpProduct in tpProductList)
                {
                    productList.Add(new Product(tpProduct));
                }
                return productList;
            }
            catch
            {
                return null;
            }
        }


    }
}