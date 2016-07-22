using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class State_DAL
    {
       
        public static TP_PACKAGE_STATE getPackageStateByName(string name)
        {
            try
            {
                var db = DBConn.createDbContext();
                var result = (from packageState in db.TP_PACKAGE_STATE
                              where packageState.NAME == name
                              select packageState).ToList();
                if (result.Count == 0)
                    return null;
                return result[0];
            }
            catch
            {
                return null;
            }
        }

        public static TP_PAYMENT_STATE getPaymentStateByName(string name)
        {
            try
            {
                var db = DBConn.createDbContext();
                var result = (from packageState in db.TP_PAYMENT_STATE
                              where packageState.NAME == name
                              select packageState).ToList();
                if (result.Count == 0)
                    return null;
                return result[0];
            }
            catch
            {
                return null;
            }
        }
    }
}