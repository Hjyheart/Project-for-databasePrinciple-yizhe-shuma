using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Data_Access_Layer.Data_View_Model;
using TPDigital.Models;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class Category_DAL
    {
        public static List<Category> getAll()
        {
            var categoryList = new List<Category>();
            OracleDbContext db = DBConn.createDbContext();
            var tpCategoryList = db.TP_CATEGORY.ToList();

            foreach (TP_CATEGORY tpCategory in tpCategoryList)
            {
                categoryList.Add(new Category(tpCategory));
            }

            return categoryList;
        }

        public static Category getByID(decimal id)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                var cate = (from ca in db.TP_CATEGORY
                            where ca.ID == id
                            select ca).ToList();
                return new Category(cate[0]);
            }
            catch
            {
                return null;
            }
        }

        public static Category getByName(string name)
        {
            OracleDbContext db = DBConn.createDbContext();
            var cate = (from ca in db.TP_CATEGORY
                        where ca.CATEGORY_NAME == name
                        select ca).ToList();
            return new Category(cate[0]);
        }

        public static List<string> getAllName()
        {
            OracleDbContext db = DBConn.createDbContext();
            var categorys = db.TP_CATEGORY;
            if (categorys != null)
            {
                List<TP_CATEGORY> res = categorys.ToList();
            }
            var names = new List<string>();
            foreach(var cate in categorys)
            {
                names.Add(cate.CATEGORY_NAME);
            }
            return names;
        }
    }
}