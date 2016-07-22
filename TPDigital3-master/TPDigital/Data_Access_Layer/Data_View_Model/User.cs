using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class User
    {
        public User()
        {

        }

        public User(string name, string phoneNum,string password, string email,List<Role> RoleList,string salt)
        {
            this.Password = password;
            this.Name = name;
            this.PhoneNumber = phoneNum;
            this.Email = email;
            this.RoleList =RoleList;
            this.Salt = salt;
            this.ID = -1;
        }

        public User(TP_USER tp)
        {
            this.tpUser = tp;
            ID = tp.ID;
            Name = tp.NAME;
            Salt = tp.SALT;
            Password = tp.PASSWORD;
            PhoneNumber = tp.PHONE_NUMBER;
            Email = tp.EMAIL;
            var db = DBConn.createDbContext();
            var icon = db.TP_IMAGE.Find(tp.ICON_ID);
            if (icon != null)
                Icon = new Image(icon);
            else
                Icon = null;
            var defalutReceiver = db.TP_RECEIVER.Find(tp.DEFAULT_RECEIVER_ID);
            if (defalutReceiver != null)
                DefaultReceiver = new Receiver(defalutReceiver);
            else
                DefaultReceiver = null;     
       
        }
        //public List<Order> addOrderListByTPUser(TP_USER tp)
        //{
        //    OrderList = new List<Order>();
        //    foreach (var order in tp.TP_ORDER)
        //    {
        //        OrderList.Add(new Order(order));
        //    }
        //    return OrderList;
        //}

        public TP_USER CreateModel()
        {
            TP_USER tp = new TP_USER();
            try
            {
                tp.NAME = Name;
                tp.SALT = Salt;
                tp.PASSWORD = Password;
                tp.PHONE_NUMBER = PhoneNumber;
                tp.EMAIL = Email;
                if (Icon == null)
                    tp.ICON_ID = null;
                else
                    tp.ICON_ID = Icon.ID;
                if (DefaultReceiver == null)
                    tp.DEFAULT_RECEIVER_ID = null;
                else
                    tp.DEFAULT_RECEIVER_ID = (decimal)DefaultReceiver.ID;
            }
            catch
            {
                return null;
            }
            return tp;
        }
        public TP_USER tpUser { get; set; }

        public decimal ID { get; set; }


        public string Name { get; set; }

        public string Salt { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Image Icon { get; set; }

        public Receiver DefaultReceiver { get; set; }

        public List<Comment> CommentList { get; set; }

        public List<Exchange> ExchangeList { get; set; }

        public List<Favorite> FavoriteList { get; set; }

        public List<Order> OrderList { get; set; }

        public List<Receiver> ReceiverList { get; set; }

        public List<Return> ReturnList { get; set; }

        public List<ShoppingCart> ShoppingCartList { get; set; }

        public List<Role> RoleList { get; set; }
    }
}