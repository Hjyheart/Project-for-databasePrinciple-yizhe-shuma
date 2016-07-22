using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class PackageState
    {
        public PackageState()
        {

        }

        public PackageState(TP_PACKAGE_STATE tp)
        {
            ID = tp.ID;
            Name = tp.NAME;
        }

        public TP_PACKAGE_STATE CreateModel()
        {
            TP_PACKAGE_STATE tp = new TP_PACKAGE_STATE();
            try
            {
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

        public List<Exchange> ExchangeList { get; set; }

        public List<Order> OrderList { get; set; }

        public List<Return> ReturnList { get; set; }
    }
}