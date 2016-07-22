using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Data_Access_Layer.Data_View_Model;
using TPDigital.Models;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class Express_DAL
    {
        public static Express getByID(decimal ID)
        {
           
            var db = DBConn.createDbContext();
            var item = db.TP_EXPRESS.Find(ID);
            if (item == null)
                return null;
            return new Express(item);
        }
        public static TP_EXPRESS getTpByID(decimal ID)
        {
            var db = DBConn.createDbContext();
            var item = db.TP_EXPRESS.Find(ID);
            return item;
        }

        public static bool Update(TP_EXPRESS item)
        {
            try
            {
                var db = DBConn.createDbContext();
                db.TP_EXPRESS.Attach(item);
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}