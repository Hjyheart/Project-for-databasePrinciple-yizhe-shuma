using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Category
    {
        public Category()
        {

        }

        public Category(TP_CATEGORY tp)
        {
            ID = tp.ID;
            CategoryName = tp.CATEGORY_NAME;
        }

        public TP_CATEGORY CreateModel()
        {
            TP_CATEGORY tp = new TP_CATEGORY();
            try
            {
                tp.CATEGORY_NAME = CategoryName;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        public decimal ID { get; set; }

        public string CategoryName { get; set; }

        //public List<Product> ProductList;
    }
}