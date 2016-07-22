using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_View_Model;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class Comment_DAL
    {
        //Select
        public static Comment getByID(long id)
        {
            OracleDbContext db = DBConn.createDbContext();
            var tpComment = db.TP_COMMENT.Find(id);
            var comment = new Comment(tpComment);
            return comment;
        }

        public static List<Comment> getByProductID(long id)
        {
            OracleDbContext db = DBConn.createDbContext();
            var tpComment = (from tpCom in db.TP_COMMENT
                                  where tpCom.PRODUCT_ID == id
                                  select tpCom).ToList();
            var comments = new List<Comment>();
            foreach (var tpCo in tpComment)
            {
                comments.Add(new Comment(tpCo));
            }
            return comments;
        }

        //Insert
        public static bool Insert(Comment comment)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                var tpComment = comment.CreateModel();
                db.TP_COMMENT.Add(tpComment);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool Insert(decimal productID, decimal orderID, decimal userID, decimal stars, string content)
        {
            try
            {
                var comment = new TP_COMMENT();
                comment.USER_ID = userID;
                comment.PRODUCT_ID = productID;
                comment.ORDER_ID = orderID;
                comment.CONTENT = content;
                comment.STARS = stars;
                comment.ADD_DATE = DateTime.Now;

                var db = DBConn.createDbContext();
                db.TP_COMMENT.Add(comment);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }


        //Update
        public static bool Update(Comment comment)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                var needComment = db.TP_COMMENT.Find(comment.ID);
                needComment.ORDER_ID = comment.OrderID;
                needComment.PRODUCT_ID = comment.ProductID;
                needComment.CONTENT = comment.Content;
                needComment.ADD_DATE = comment.AddDate;
                needComment.USER_ID = comment.UserID;
                needComment.STARS = comment.Stars;
                db.TP_COMMENT.Attach(needComment);
                db.Entry(needComment).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool Update(TP_COMMENT comment)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                db.TP_COMMENT.Attach(comment);
                db.Entry(comment).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Delete
        public static bool Delete(decimal id)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                var comment = db.TP_COMMENT.Find(id);
                db.TP_COMMENT.Remove(comment);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;


        }

        public static bool DeleteByProductID(decimal id)
        {
            try
            {
                OracleDbContext db = DBConn.createDbContext();
                var tpComment = (from tpCom in db.TP_COMMENT
                                 where tpCom.PRODUCT_ID == id
                                 select tpCom).ToList();
                foreach (var tpCo in tpComment)
                {
                    db.TP_COMMENT.Remove(tpCo);
                }
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;

        }
    }
}