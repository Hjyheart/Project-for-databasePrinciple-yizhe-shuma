using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using TPDigital.Data_Access_Layer.Data_Access_Layer;
using TPDigital.Models;

//暂不处理有效期超过活动期限的情况
//通过getRealTimePrice(decimal productid, decimal originprice)获取商品的实时价格
//通过insertNewActivity(decimal activityid)通知Cache更新活动列表

namespace TPDigital.Cache
{
    public class ProRealTimePriceCache
    {
        private static Dictionary<decimal, decimal> Prices = null;
        private static List<TP_ACTIVITY> Activitys = null;
        private static Dictionary<decimal, List<TP_ACTIVITY_PRODUCT>> ActivityProducts = null;

        private static Dictionary<decimal, DateTime> PriceExpire = null;
        private static Dictionary<decimal, DateTime> ActivityExpire = null;

        private const int _MAX_COUNT = 1000;

        public static decimal getRealTimePrice(decimal productid, decimal originprice)
        {
            if (Activitys == null)
            {
                inits();
            }

            if (Prices.ContainsKey(productid) && PriceExpire[productid] > DateTime.Now)
            {
                return Prices[productid];
            }
            else
            {
                PriceExpire[productid] = DateTime.Now.AddHours(0.5);
                return Prices[productid] = getCurrentPrice(productid, originprice);
            }
            return 0;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="productid"></param>
        /// <param name="originprice"></param>
        /// <returns></returns>
        private static decimal getCurrentPrice(decimal productid, decimal originprice)
        {
            decimal lowestdiscount = 1;
            for (int i = 0; i < Activitys.Count(); i++)
            {
                TP_ACTIVITY act = Activitys[i];

                //判断是否超过有效期
                if (ActivityExpire[act.ID] < DateTime.Now)
                {
                    act = Activity_DAL.getTPByID(act.ID);
                    if (act.END_TIME > DateTime.Now)
                    {
                        Activitys[i] = act;
                        ActivityProducts[act.ID] = Activity_Product_DAL.getListByActivityID(act.ID, 1, _MAX_COUNT);
                        ActivityExpire[act.ID] = DateTime.Now.AddHours(1) > act.END_TIME
                            ? act.END_TIME
                            : DateTime.Now.AddHours(1);
                    }
                    else act = null;
                }

                //判断活动是否可用
                if (act != null)
                {
                    foreach (var act_pro in ActivityProducts[act.ID])
                    {
                        if (act_pro.PRODUCT_ID == productid && lowestdiscount > act.DISCOUNT)
                        {
                            lowestdiscount = act.DISCOUNT;
                        }
                    }
                }
                //不生效的活动删除
                else
                {
                    ActivityProducts.Remove(Activitys[i].ID);
                    Activitys.RemoveAt(i);
                }
            }
            return lowestdiscount*originprice;
        }

        public static void setForceUpdateActivity(decimal activityid)
        {
            if (Activitys == null) return;
            TP_ACTIVITY newact = Activity_DAL.getTPByID(activityid);
            if (newact.END_TIME > DateTime.Now)
            {
                ActivityExpire[activityid] = DateTime.Now.AddHours(-1);
                if (!Activitys.Contains(newact))
                {
                    Activitys.Add(newact);
                }
            }
        }

        public static void setForceUpdateProduct(decimal productid)
        {
            if (Activitys == null) return;
            PriceExpire[productid] = DateTime.Now.AddHours(-1);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private static void initAllActivity()
        {
            Activitys = Activity_DAL.getListByEndTime(DateTime.Now, 1, _MAX_COUNT);
            ActivityProducts = new Dictionary<decimal, List<TP_ACTIVITY_PRODUCT>>();
            ActivityExpire = new Dictionary<decimal, DateTime>();
            foreach (var act in Activitys)
            {
                ActivityExpire[act.ID] = DateTime.Now.AddHours(1) > act.END_TIME
                    ? act.END_TIME
                    : DateTime.Now.AddHours(1);
                ActivityProducts[act.ID] = Activity_Product_DAL.getListByActivityID(act.ID, 1, _MAX_COUNT);
            }
        }

        private static void inits()
        {
            Prices = new Dictionary<decimal, decimal>();
            PriceExpire = new Dictionary<decimal, DateTime>();
            initAllActivity();
        }
    }
}