using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Data_Access_Layer.Data_View_Model;
using TPDigital.Models;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class Receiver_DAL
    {
        //得到用户的所有收货地址
        public static List<Receiver> getAll(decimal userID)
        {
            var receiverList = new List<Receiver>();
            var db = DBConn.createDbContext();
            var tpReceivers = (from tpReceiver in db.TP_RECEIVER
                               where tpReceiver.USER_ID == userID
                               select tpReceiver).ToList();

            foreach (TP_RECEIVER tpReceiver in tpReceivers)
            {
                receiverList.Add(new Receiver(tpReceiver));
            }

            return receiverList;
        }

        public static List<Receiver> getListByUser(TP_USER user)
        {
            List<Receiver> receiverList = new List<Receiver>();
            foreach (var receiver in user.TP_RECEIVER)
            {
                receiverList.Add(new Receiver(receiver));
            }
            return receiverList;
        }
        public static List<Receiver> getListNoDefault(TP_USER user)
        {
            List<Receiver> receiverList = new List<Receiver>();
            foreach (var receiver in user.TP_RECEIVER)
            {
                if(receiver.ID != user.DEFAULT_RECEIVER_ID)
                    receiverList.Add(new Receiver(receiver));
            }
            return receiverList;
        }

        public static TP_RECEIVER getByID(decimal ID)
        {
            var db = DBConn.createDbContext();
            var item = db.TP_RECEIVER.Find(ID);
            return item;
        }
        public static Receiver getReceiverByID(decimal ID)
        {
            var item = getByID(ID);
            if (item == null)
                return null;
            return new Receiver(item);
        }
        public static decimal insert(decimal locationID, decimal userID, string phone, string name, string addrDetail)
        {
            try
            {
                var db = DBConn.createDbContext();
                TP_RECEIVER item = new TP_RECEIVER();
                item.LOCATION_ID = locationID;
                item.USER_ID = userID;
                item.PHONE_NUMBER = phone;
                item.NAME = name;
                item.ADDR_DETAIL = addrDetail;

                db.TP_RECEIVER.Add(item);
                db.Entry(item).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();

                return item.ID;
            }
            catch
            {
                return -1;
            }
        }
        public static bool update(TP_RECEIVER item)
        {
            try
            {
                var db = DBConn.createDbContext();
                db.TP_RECEIVER.Attach(item);
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool deleteByID(decimal ID)
        {
            var db = DBConn.createDbContext();
            var item = getByID(ID);
            if (item == null)
                return false;

            try
            {
                item.USER_ID = 0;
                db.TP_RECEIVER.Attach(item);
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