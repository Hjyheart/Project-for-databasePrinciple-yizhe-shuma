using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using TPDigital.Data_Access_Layer.Data_View_Model;
using TPDigital.Models;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class Order_DAL
    {

        public static bool update(TP_ORDER item)
        {
            try
            {
                var db = DBConn.createDbContext();
                db.TP_ORDER.Attach(item);
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Order getByID(decimal id)
        {
            var db = DBConn.createDbContext();
            var newOrder = db.TP_ORDER.Find(id);
            if (newOrder == null)
                return null;
            return new Order(newOrder);
        }

        public static TP_ORDER getTpOrderByID(decimal id)
        {
            var db = DBConn.createDbContext();
            var newOrder = db.TP_ORDER.Find(id);
            return newOrder;
        }

        public static List<Order> getListByUser(TP_USER user)
        {
            List<Order> orderList = new List<Order>();
            var orders = (from order in user.TP_ORDER
                          orderby order.ADD_DATE descending
                          select order).ToList();

            foreach (var order in orders)
            {
                Order newOrder = new Order(order);
                newOrder.addOrderProductList();
                orderList.Add(newOrder);
            }
            return orderList;
        }

        public static decimal createTP_ORDER(decimal totalPrice, decimal reveiver_id, decimal user_id)
        {
            //USER_ID SHIPPING_RECEIVER_ID *EXPRESS_ID    PACKAGE_STATE_ID  PAYMENT_STATE_ID TOTAL_PRICE   ADD_DATE  *SIGN_TIME
            TP_ORDER order = new TP_ORDER();
            //order.ID = 123456;
            order.USER_ID = user_id;
            order.SHIPPING_RECEIVER_ID = reveiver_id;
            order.EXPRESS_ID = null;

            order.PACKAGE_STATE_ID = State_DAL.getPackageStateByName("待出库").ID;
            order.PAYMENT_STATE_ID = State_DAL.getPaymentStateByName("待付款").ID;

            order.TOTAL_PRICE = totalPrice;
            order.ADD_DATE = DateTime.Now;

            var db = DBConn.createDbContext();
            db.TP_ORDER.Add(order);
            db.Entry(order).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();

            return (decimal)order.ID;

        }

        public static bool createProductOrderList(List<ShoppingCart> shoppingCartList, decimal orderID)
        {
            try
            {
                for (int i = 0; i < shoppingCartList.Count; i++)
                {
                    var productOrder = new TP_ORDER_PRODUCT();
                    productOrder.ORDER_ID = orderID;
                    productOrder.PRODUCT_ID = shoppingCartList[i].Product.ID;
                    productOrder.PRICE = shoppingCartList[i].Product.Price;
                    productOrder.QUANTITY = shoppingCartList[i].Quantity;
                    var db = DBConn.createDbContext();
                    db.TP_ORDER_PRODUCT.Add(productOrder);
                    db.Entry(productOrder).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static List<Order> getPageList(int pageIndex, int pageSize, ref int totalPages,int state=-1)
        {
            try
            {
                int startRow = (pageIndex - 1) * pageSize;
                var orderList = new List<Order>();
                var db = DBConn.createDbContext();
                List<TP_ORDER> tpOrderList = new List<TP_ORDER>();
                IOrderedQueryable<TP_ORDER> tmp = null;
                var test = tmp.ToList();
                if (state == -1)
                    tmp = db.TP_ORDER.OrderBy(item => item.ID);
                else
                    tmp = db.TP_ORDER.Where(item => item.PACKAGE_STATE_ID == (decimal)state || item.PAYMENT_STATE_ID == (decimal)state).OrderBy(item => item.ID);
                tpOrderList = tmp.Skip(startRow).Take(pageSize).ToList();
                if(tmp!=null)
                    totalPages = (tmp.ToList().Count + pageSize - 1) / pageSize;
                foreach (TP_ORDER tpOrder in tpOrderList)
                {
                    orderList.Add(new Order(tpOrder));
                }
                return orderList;
            }
            catch
            {
                return null;
            }
        }

        public static bool UpdateState(decimal orderID, decimal stateID)
        {
            try
            {
                var db = DBConn.createDbContext();
                var tpOrder = db.TP_ORDER.Find(orderID);
                tpOrder.PACKAGE_STATE_ID = stateID;
                db.TP_ORDER.Attach(tpOrder);
                db.Entry(tpOrder).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static int getTotalPage(int pageIndex, int pageSize)
        {
            try
            {
                var db = DBConn.createDbContext();
                int totalCnt = db.TP_ORDER.ToList().Count;//待测试
                int totalPage = (totalCnt + pageSize - 1) / pageSize;
                return totalPage;
            }
            catch
            {
                return -1;
            }
        }
    }

}