using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class PaymentState
    {
        public PaymentState()
        {

        }

        public PaymentState(TP_PAYMENT_STATE tp)
        {
            ID = tp.ID;
            Name = tp.NAME;
        }

        public TP_PAYMENT_STATE CreateModel()
        {
            TP_PAYMENT_STATE tp = new TP_PAYMENT_STATE();
            try
            {
                //tp.ID = (decimal)ID;
                tp.NAME = Name;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        public decimal ID { get; set; }

        public string Name { get; set; }

        public List<Order> OrderList { get; set; }

    }
}