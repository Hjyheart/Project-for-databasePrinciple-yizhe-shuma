using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{


    public class ProductComparer : IComparer<HotProduct>
    {
        public int Compare(HotProduct a, HotProduct b)
        {
            if (a.productID == b.productID)
                return 0;
            if (a.clickNum < b.clickNum)
                return 1;
            else 
                return -1;
           
        }
    }

    public class HotProduct
    {
        public int clickNum { get; set; }
        public decimal productID { get; set; }

        public HotProduct(decimal id)
        {
            productID = id;
            clickNum = 1;
            //clickNum++;
        }
    }

    public static class HotProducts
    {
        public static SortedSet<HotProduct> hotProducts = new SortedSet<HotProduct>(new ProductComparer());

        public static bool inital = false;

        static public List<decimal> getIDList()
        {
            var list = new List<decimal>();
            foreach(var hp in hotProducts)
            {
                list.Add(hp.productID);
            }
            return list;
        }
    }


    
}