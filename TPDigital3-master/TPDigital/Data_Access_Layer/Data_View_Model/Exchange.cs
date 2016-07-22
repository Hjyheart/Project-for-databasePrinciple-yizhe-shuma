using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Exchange
    {
        public Exchange()
        {
            
        }

        public Exchange(TP_EXCHANGE tp)
        {
            ID = tp.ID;
            //Order = new Order(DBConn.db.TP_ORDER.Find(tp.ORDER_ID));
            OrderID = tp.ORDER_ID;
            //Product = new Product(DBConn.db.TP_PRODUCT.Find(tp.PRODUCT_ID));
            ProductID = tp.PRODUCT_ID;
            //User = new User(DBConn.db.TP_USER.Find(tp.USER_ID));
            UserID = tp.USER_ID;
            Quantity = tp.QUANTITY;
            var db = DBConn.createDbContext();
            PackageState = new PackageState(db.TP_PACKAGE_STATE.Find(tp.PACKAGE_STATE));
            Express = new Express(db.TP_EXPRESS.Find(tp.EXPRESS_ID));
            ApplyDate = tp.APPLY_DATE;
            ReturnExpress = new Express(db.TP_EXPRESS.Find(tp.RETURN_EXPRESS_ID));
        }

        public TP_EXCHANGE CreateModel()
        {
            TP_EXCHANGE tp = new TP_EXCHANGE();
            try
            {
                tp.ORDER_ID = OrderID;
                tp.PRODUCT_ID = ProductID;
                tp.USER_ID = UserID;
                tp.QUANTITY = Quantity;
                tp.PACKAGE_STATE = PackageState.ID;
                tp.EXPRESS_ID = Express.ID;
                tp.APPLY_DATE = ApplyDate;
                tp.RETURN_EXPRESS_ID = ReturnExpress.ID;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        public decimal ID { get; set; }

        public decimal OrderID { get; set; }

        public decimal ProductID { get; set; }

        public decimal UserID { get; set; }

        public decimal Quantity { get; set; }

        public PackageState PackageState { get; set; }
        public Express Express { get; set; }

        public DateTime ApplyDate { get; set; }

        public Express ReturnExpress { get; set; }
    }
}