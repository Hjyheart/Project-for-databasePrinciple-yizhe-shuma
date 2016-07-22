using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Location
    {
        public Location()
        {

        }

        public Location(TP_LOCATION tp)
        {
            ID = tp.ID;
            PrivinceName = tp.PRIVINCE_NAME;
            CityName = tp.CITY_NAME;
        }

        public TP_LOCATION CreateModel()
        {
            TP_LOCATION tp = new TP_LOCATION();
            try
            {
                tp.PRIVINCE_NAME = PrivinceName;
                tp.CITY_NAME = CityName;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        public decimal ID { get; set; }

        public string PrivinceName { get; set; }

        public string CityName { get; set; }
    }
}