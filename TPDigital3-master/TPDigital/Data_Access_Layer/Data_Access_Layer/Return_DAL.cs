using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Data_Access_Layer.Data_View_Model;
using TPDigital.Models;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class Return_DAL
    {
        public static List<Return> getListByUser(TP_USER user)
        {
            List<Return> returnList = new List<Return>();
            foreach (var item in user.TP_RETURN)
            {
                Return newReturn = new Return(item);
                newReturn.Product.setProductImageList();
                returnList.Add(newReturn);
            }
            return returnList;
        }
        public static TP_RETURN getTpByID(decimal returnID)
        {
            var db = DBConn.createDbContext();
            var item = db.TP_RETURN.Find(returnID);
            return item;
        }
        public static Return getByID(decimal returnID)
        {
            var item = getTpByID(returnID);
            if (item == null)
                return null;
            return new Return(item);
        }
    }
}