using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Order
    {
        public Order()
        {

        }

        public Order(TP_ORDER tp)
        {
            this.tpOdrder = tp;
            ID = tp.ID;
            this.UserID = tp.USER_ID;
            var db = DBConn.createDbContext();
            UserName = db.TP_USER.Find(UserID).NAME;
            ShippingReceiver = new Receiver(db.TP_RECEIVER.Find(tp.SHIPPING_RECEIVER_ID));
            var express = db.TP_EXPRESS.Find(tp.EXPRESS_ID);
            if (express != null)
                Express = new Express();
                PackageState = new PackageState(db.TP_PACKAGE_STATE.Find(tp.PACKAGE_STATE_ID));
            PaymentState = new PaymentState(db.TP_PAYMENT_STATE.Find(tp.PAYMENT_STATE_ID));
            TotalPrice = tp.TOTAL_PRICE;
            AddDate = tp.ADD_DATE;
            if (tp.SIGN_TIME != null)
                SignTime = (DateTime)tp.SIGN_TIME;
            this.OrderProductList = new List<Order_Product>();
        }
        public List<Order_Product> addOrderProductList(){
            this.OrderProductList.Clear();
            foreach(var productOrder in this.tpOdrder.TP_ORDER_PRODUCT){
                var item = new Order_Product(productOrder);
                item.Product.setProductImageList();
                this.OrderProductList.Add(item);
            }
            return this.OrderProductList;
        }

        public TP_ORDER CreateModel()
        {
            TP_ORDER tp = new TP_ORDER();
            try
            {
                tp.ID = ID;
                tp.USER_ID = this.UserID;
                tp.SHIPPING_RECEIVER_ID = ShippingReceiver.ID;
                tp.EXPRESS_ID = Express.ID;
                tp.PACKAGE_STATE_ID = PackageState.ID;
                tp.PAYMENT_STATE_ID = PaymentState.ID;
                tp.TOTAL_PRICE = TotalPrice;
                tp.ADD_DATE = AddDate;
                tp.SIGN_TIME = SignTime;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        public TP_ORDER tpOdrder { get; set; }
        public decimal ID { get; set; }

       // public User User { get; set; }

        public decimal UserID { get; set; }

        public string UserName { get; set; }

        public Receiver ShippingReceiver { get; set; }

        public Express Express { get; set; }

        public PackageState PackageState { get; set; }

        public PaymentState PaymentState { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime AddDate { get; set; }

        public DateTime SignTime { get; set; }

        public List<Comment> CommentList { get; set; }

        public List<Exchange> ExchangeList { get; set; }

        public List<Order_Product> OrderProductList { get; set; }

        public List<Return> ReturnList { get; set; }


    }
}