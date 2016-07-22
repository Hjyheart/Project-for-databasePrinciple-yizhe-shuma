using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_View_Model;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class ShoppingCart_DAL
    {
        public static ShoppingCart getByID(decimal id)
        {
            try
            {
                var db = DBConn.createDbContext();
                TP_SHOPPING_CART tpShoppingCart = db.TP_SHOPPING_CART.Find(id);
                return new ShoppingCart(tpShoppingCart);
            }
            catch
            {
                return null;
            }
        }

        public static TP_SHOPPING_CART getTpShoppingCartByID(decimal id)
        {
            var db = DBConn.createDbContext();
            TP_SHOPPING_CART tpShoppingCart = db.TP_SHOPPING_CART.Find(id);
            return tpShoppingCart;
        }

        public static List<ShoppingCart> getListByIDList(List<decimal> idList)
        {
            try
            {
                List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
                foreach (decimal id in idList)
                {
                    var shoppingCart = ShoppingCart_DAL.getByID(id);
                    shoppingCartList.Add(shoppingCart);
                }
                return shoppingCartList;
            }
            catch
            {
                return null;
            }
           
        }
        public static decimal getShoppingCartIDByUserProduct(decimal userID, decimal productID)
        {
            try
            {
                var db = DBConn.createDbContext();
                var tpShoppingCart = (from tpSC in db.TP_SHOPPING_CART
                                      where tpSC.USER_ID == userID && tpSC.PRODUCT_ID == productID
                                      select tpSC).ToList();
                if (tpShoppingCart.Count == 1)
                    return tpShoppingCart[0].ID;
                else if (tpShoppingCart.Count == 0)
                    return -1;
            }
            catch
            {
                return -1;
            }
            return -2;//数据库中先前的判重出了问题
        }

        public static List<ShoppingCart> getAll(decimal userID)
        {
            var shoppingCarts = new List<ShoppingCart>();
            var db = DBConn.createDbContext();
            var tpShoppingCarts = (from shoppingCart in db.TP_SHOPPING_CART
                                      where shoppingCart.USER_ID == userID
                                      select shoppingCart).ToList();
            foreach (TP_SHOPPING_CART tpShoppingCart in tpShoppingCarts)
            {
                shoppingCarts.Add(new ShoppingCart(tpShoppingCart));
            }
            return shoppingCarts;
        }

        //Update
        public static int Update(ShoppingCart shoppingCart)
        {
            try
            {
                var db = DBConn.createDbContext();
                var newShoppingCart = db.TP_SHOPPING_CART.Find(shoppingCart.ID);
                newShoppingCart = shoppingCart.CreateModel();
                newShoppingCart.USER_ID = shoppingCart.UserID;
                newShoppingCart.PRODUCT_ID = shoppingCart.Product.ID;
                newShoppingCart.QUANTITY = shoppingCart.Quantity;
                db.TP_SHOPPING_CART.Attach(newShoppingCart);
                db.Entry(newShoppingCart).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                return 0;
            } 
            return 1;
        }


        //Insert
        public static int Insert(ShoppingCart shoppingCart)
        {
            //合并后修改
            if(shoppingCart.Quantity<1||shoppingCart.Product.Inventory<shoppingCart.Quantity)
                return 0;
            try
            {
                var db = DBConn.createDbContext();
                var newShoppingCart = shoppingCart.CreateModel();
                db.TP_SHOPPING_CART.Add(newShoppingCart);
                //DBConn.db.Entry(newShoppingCart).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
            }
            catch
            {
                return 0;
            } 
            return 1;
        }

        //Delete
        public static bool Delete(decimal id)
        {
            try
            {
                var db = DBConn.createDbContext();
                var shoppingCart = db.TP_SHOPPING_CART.Find(id);
                db.TP_SHOPPING_CART.Remove(shoppingCart);
                //DBConn.db.Entry(shoppingCart).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //单独处理数量
        public static int UpdateQuantity(decimal shoppingCartID, int count)
        {
            int result = -1;
            try
            {
                var db = DBConn.createDbContext();
                var newShoppingCart = db.TP_SHOPPING_CART.Find(shoppingCartID);
                var shoppingCart = new ShoppingCart(newShoppingCart);
                if (count >= 0)
                {
                    if (count <= shoppingCart.Product.Inventory)
                    {
                        newShoppingCart.QUANTITY = count;
                    }
                    else
                    {
                        newShoppingCart.QUANTITY = shoppingCart.Product.Inventory;
                        result = Convert.ToInt32(newShoppingCart.QUANTITY);
                    }
                }
                else
                {
                    result = -2;
                }
                db.TP_SHOPPING_CART.Attach(newShoppingCart);
                db.Entry(newShoppingCart).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                return -2;
            }
            return result;
        }


        public static int AddQuantity(decimal shoppingCartID, int add)
        {
            try
            {
                var db = DBConn.createDbContext();
                var newShoppingCart = db.TP_SHOPPING_CART.Find(shoppingCartID);
                var product = db.TP_PRODUCT.Find(newShoppingCart.PRODUCT_ID);
                if (add > 0 && newShoppingCart.QUANTITY + add <= product.INVENTORY)
                    newShoppingCart.QUANTITY += add;
                else
                    return 0;
                db.TP_SHOPPING_CART.Attach(newShoppingCart);
                db.Entry(newShoppingCart).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                return 0;
            }
            return 1;
        }

        //新增
        public static bool Delete(TP_SHOPPING_CART item)
        {
            try
            {

                var db = DBConn.createDbContext();
                db.TP_SHOPPING_CART.Remove(item);
                //DBConn.db.Entry(shoppingCart).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        //新增
        public static TP_SHOPPING_CART getByProductIDUserID(decimal productID, decimal userID)
        {

            var db = DBConn.createDbContext();
            List<TP_SHOPPING_CART> itemList = (from item in db.TP_SHOPPING_CART
                                               where item.PRODUCT_ID == productID && item.USER_ID == userID
                                               select item).ToList();
            if (itemList.Count == 0)
                return null;
            return itemList[0];
        }

    }
}