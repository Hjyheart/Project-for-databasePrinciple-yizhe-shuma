using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qiniu;
using Qiniu.RS;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;
using Newtonsoft.Json;

namespace TPDigital.Controllers
{
    public class UpJson
    {
        public string uptoken { get; set; }
    }

    
    public class CallbackJson
    {
        public CallbackJson(decimal id, string url)
        {
            ID = Convert.ToString(id);
            URL = url;
        }
        public string ID { get; set; }
        public string URL { get; set; }
    }

    public class UploadController : Controller
    {
        // GET: Update
        public JsonResult Index()
        {
            var upinf = new UpJson();

            Qiniu.Conf.Config.ACCESS_KEY = "0kgFvvZPnN4KioE-Dah_t2MtLWH0S3ZREmXArbMI";
            Qiniu.Conf.Config.SECRET_KEY = "_ZXb50vG-9ZWP6METCCYfGrt632UlaGr6uZZdqNi";
            String bucket = "kanxuan";
           
            PutPolicy put = new PutPolicy(bucket, 3600);

            put.CallBackUrl = "http://yizhe.picfood.cn/upload/callback";
            put.CallBackBody = "filename=$(fname)&filesize=$(fsize)&fileskey=$(key)";

            upinf.uptoken = put.Token();

            return Json(upinf, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CallBack()
        {
            //Request.Form.
            if (Request.Form["fileskey"]==null)
            {
                return Json("error");
            }
            var url = "http://oae23pecn.bkt.clouddn.com/" + Request.Form["fileskey"];
            var imageID = Image_DAL.InsertByUrl(url);
            return Json(new CallbackJson(imageID, url));
        }

    }
}