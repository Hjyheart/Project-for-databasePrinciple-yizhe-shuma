using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Return
    {
        public Return()
        {
        }

        public Return(TP_RETURN tp)
        {
            ID = tp.ID;
            this.OrderID = tp.ORDER_ID;
            var db = DBConn.createDbContext();
            Product = new Product(db.TP_PRODUCT.Find(tp.PRODUCT_ID));
            this.UserID = tp.USER_ID;
            Quantity = tp.QUANTITY;
            PackageState = new PackageState(db.TP_PACKAGE_STATE.Find(tp.PACKAGE_STATE));
            Express = new Express(db.TP_EXPRESS.Find(tp.EXPRESS_ID));
            ApplyDate = tp.APPLY_DATE;
        }

        public TP_RETURN CreateModel()
        {
            TP_RETURN tp = new TP_RETURN();
            try
            {
                tp.ORDER_ID = this.OrderID;
                tp.PRODUCT_ID = Product.ID;
                tp.USER_ID = this.UserID;
                tp.QUANTITY = Quantity;
                tp.PACKAGE_STATE = PackageState.ID;
                tp.EXPRESS_ID = Express.ID;
                tp.APPLY_DATE = ApplyDate;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        public decimal ID { get; set; }

        //public Order Order { get; set; }
        public decimal OrderID { get; set; }

        public Product Product { get; set; }

    //    public User User { get; set; }
        public decimal UserID { get; set; }

        public decimal Quantity { get; set; }

        public PackageState PackageState { get; set; }

        public Express Express { get; set; }

        public DateTime ApplyDate { get; set; }
    }
}