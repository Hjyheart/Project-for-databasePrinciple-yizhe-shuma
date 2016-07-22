using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class Location_DAL
    {
        public static TP_LOCATION getByID(decimal ID)
        {
            var db = DBConn.createDbContext();
            var item = db.TP_LOCATION.Find(ID);
            return item;
        }
        public static TP_LOCATION getByProvinceAndCity(string province, string city)
        {
            var db = DBConn.createDbContext();
            List<TP_LOCATION> itemList = (from item in db.TP_LOCATION
                                          where item.PRIVINCE_NAME == province && item.CITY_NAME == city
                                          select item).ToList();
            if (itemList.Count == 0)
                return null;
            return itemList[0];
        }
        public static List<string> getCityByProvince(string province)
        {
            var db = DBConn.createDbContext();
            var items = (from item in db.TP_LOCATION
                         where item.PRIVINCE_NAME == province
                         select item.CITY_NAME).Distinct().ToList();
            return items;
        }
        public static List<string> getProvince()
        {
            var db = DBConn.createDbContext();
            var items = (from item in db.TP_LOCATION
                         select item.PRIVINCE_NAME).Distinct().ToList();
            return items;
        }
    }
}