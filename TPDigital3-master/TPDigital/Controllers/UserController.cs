using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;
using System.Web.Security;
using TPDigital.Data_Access_Layer.Data_View_Model;
using System.Security.Cryptography;
using Top.Api.Request;
using Top.Api;
using Top.Api.Response;

namespace TPDigital.Controllers
{


    [MyAuthorizeAttribute(Roles = "管理员,顾客")]
    public class UserController : Controller
    {
        [AllowAnonymous]
        public ActionResult LoginView()
        {
            if (User.Identity.Name != "")
            {
                ViewBag.state = 1;
            }
            else
            {
                ViewBag.state = 0;
            }
            ViewBag.categoryList = Category_DAL.getAll();
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public int LoginView(FormCollection fc, string email, string password)
        {
            var user = User_DAL.getByPhonenum(email);
            ////////////////
            //if (user == null || user.Password != password)
            //    return 0;

            ////////////////////调试代码
            if (user == null || user.Password != encrypt(password, user.Salt))
                return 0;
            else
            {
                var remember_me = Request.Form["remember-me"];
                FormsAuthentication.SetAuthCookie(user.ID.ToString(), remember_me != null && remember_me == "true");
                return 1;
            }
        }

        //register
        [AllowAnonymous]
        public ActionResult RegisterView()
        {
            if (User.Identity.Name != "")
            {
                ViewBag.state = 1;
            }
            else
            {
                ViewBag.state = 0;
            }
            ViewBag.categoryList = Category_DAL.getAll();
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public int Register(FormCollection fc, string email, string password, string name, string phone, string vercode)
        {
            if (!checkVerCode(vercode, phone))
                return 0;

            var role = Role_DAL.getByName("顾客");
            List<Role> roleList = new List<Role> { role };
            string salt = getRandomString(10);
            password = encrypt(password, salt);
            int resultStatus = User_DAL.Insert(new User(name, phone, password, email, roleList, salt));
            ViewBag.categoryList = Category_DAL.getAll();
            return resultStatus;
        }
        public ActionResult Logout()
        {
            if (User.Identity.Name != "")
            {
                ViewBag.state = 1;
            }
            else
            {
                ViewBag.state = 0;
            }
            FormsAuthentication.SignOut();
            return new RedirectResult("../User/LoginView");
        }

        //地址界面
        //检验用户是否登陆后列出用户地址
        //界面包含地址添加、删除按钮
        // GET: /Account/Addr
        public ActionResult Addr()
        {
            if (User.Identity.Name != "")
            {
                ViewBag.state = 1;
            }
            else
            {
                ViewBag.state = 0;
            }
            
            var user = User_DAL.getTpUserByID(decimal.Parse(User.Identity.Name));
            List<Receiver> receiverList = Receiver_DAL.getListNoDefault(user);
            if (user.DEFAULT_RECEIVER_ID == null)
                ViewBag.defalutReceiver = null;
            else
                ViewBag.defalutReceiver = Receiver_DAL.getReceiverByID((decimal)user.DEFAULT_RECEIVER_ID);
            ViewBag.provinces = Location_DAL.getProvince();
            ViewBag.categoryList = Category_DAL.getAll();
            return View(receiverList);
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult getCity(FormCollection fc, string province)
        {
            var items = Location_DAL.getCityByProvince(province);
            return Json(items);
        }

        [HttpPost]
        public int AddAddr()
        {
            try
            {
                decimal locationID = Location_DAL.getByProvinceAndCity(Request.Form["privinceName"], Request.Form["cityName"]).ID;
                var user = User_DAL.getTpUserByID(decimal.Parse(User.Identity.Name));

                decimal receiverID = Receiver_DAL.insert(locationID, user.ID, Request.Form["phoneNumber"], Request.Form["name"], Request.Form["addrDetail"]);
                if (receiverID == -1)
                    return 0;

                if (user.DEFAULT_RECEIVER_ID == null)
                    user.DEFAULT_RECEIVER_ID = receiverID;
                return (int)receiverID==-1?0:1;
            }
            catch
            {
                return 0;
            }
        }


        [HttpPost]
        public int UpdateAddr(FormCollection fc, decimal receiverID, string name, string addrDetail, string phoneNumber, string privinceName, string cityName)
        {
            try
            {
                decimal locationID = Location_DAL.getByProvinceAndCity(privinceName, cityName).ID;
                var user = User_DAL.getTpUserByID(decimal.Parse(User.Identity.Name));

                var receiver = Receiver_DAL.getByID(receiverID);

                receiver.NAME = name;
                receiver.LOCATION_ID = locationID;
                receiver.PHONE_NUMBER = phoneNumber;
                receiver.ADDR_DETAIL = addrDetail;
                Receiver_DAL.update(receiver);

                if (receiverID == -1)
                    return 0;
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        [HttpPost]
        public int DeleteAddr(FormCollection fc, decimal receiverID)
        {
            var user = User_DAL.getTpUserByID(decimal.Parse(User.Identity.Name));
            var deleteResult = Receiver_DAL.deleteByID(receiverID);
            if (!deleteResult)
                return 0;
            if (user.DEFAULT_RECEIVER_ID == receiverID)
            {
                user.DEFAULT_RECEIVER_ID = null;
                User_DAL.Update(user);
            }
            return 1;
        }

        [HttpPost]
        public int setDefaultAddr(FormCollection fc, decimal id)
        {
            var user = User_DAL.getTpUserByID(decimal.Parse(User.Identity.Name));
            user.DEFAULT_RECEIVER_ID = id;
            var state = User_DAL.Update(user);
            return state ? 1 : 0;
        }     

        //个人信息显示界面
        public ActionResult ShowInf()
        {
            if (User.Identity.Name != "")
            {
                ViewBag.state = 1;
            }
            else
            {
                ViewBag.state = 0;
            }
            var user = User_DAL.getByID(decimal.Parse(User.Identity.Name));
            ViewBag.categoryList = Category_DAL.getAll();
            return View(user);
        }

        ////修改
        //修改信息提交链接
        [HttpPost]
        public int ChangeName(FormCollection fc, string name)
        {
            if (name == null || name == "")
                return 0;
            var user = User_DAL.getTpUserByID(decimal.Parse(User.Identity.Name));
            user.NAME = name;

            return User_DAL.Update(user) ? 1 : 0;
        }
        [HttpPost]
        public int ChangePhone(FormCollection fc, string phone, string vercode)
        {
            if (phone == null || phone == "" || !checkVerCode(vercode, phone))
                return 0;
            var user = User_DAL.getTpUserByID(decimal.Parse(User.Identity.Name));
            user.PHONE_NUMBER = phone;

            return User_DAL.Update(user) ? 1 : 0;
        }
        [HttpPost]
        public int ChangeEmail(FormCollection fc, string email)
        {
            if (email == null || email == "")
                return 0;
            var user = User_DAL.getTpUserByID(decimal.Parse(User.Identity.Name));
            user.EMAIL = email;

            return User_DAL.Update(user) ? 1 : 0;
        }
        [HttpPost]
        public int ChangePassword(FormCollection fc, string oldPassword, string newPassword)
        {
            if (oldPassword == null || newPassword == null || oldPassword == "" || newPassword == "")
                return 0;
            var user = User_DAL.getTpUserByID(decimal.Parse(User.Identity.Name));
            if (user.PASSWORD != encrypt(oldPassword, user.SALT))
                return 0;
            user.PASSWORD = encrypt(newPassword, user.SALT);
            return User_DAL.Update(user) ? 1 : 0;
        }




        /// <summary>
        ///加密代码
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        private string getRandomString(int len)
        {
            string s = "123456789abcdefghijklmnpqrstuvwxyzABCDEFGHIJKLMNPQRSTUVWXYZ";
            string reValue = string.Empty;
            Random rnd = new Random();
            while (reValue.Length < len)
            {
                string s1 = s[rnd.Next(0, s.Length)].ToString();
                reValue += s1;
            }
            return reValue;
        }

        private string encrypt(string password, string ranstr)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] origin = System.Text.Encoding.Default.GetBytes(password + ranstr);
            byte[] res = md5.ComputeHash(origin);
            return System.Text.Encoding.Default.GetString(res);
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        private static Dictionary<decimal, string> vercordes = new Dictionary<decimal, string>();
        private bool checkVerCode(string vercode, string phone_number)
        {
            decimal id = decimal.Parse(phone_number);
            if (vercordes.ContainsKey(id) && vercordes[id] == vercode)
            {
                vercordes.Remove(id);
                return true;
            }
            return false;
        }

        [AllowAnonymous]
        public int sendMessage(string phone_number)
        {
            try
            {
                ITopClient client = new DefaultTopClient("http://gw.api.taobao.com/router/rest", "23384534", "a6a09f7472d6f47baa85879160577348");
                AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();

                string vercode = getRandomString(6);
                vercordes[decimal.Parse(phone_number)] = vercode;

                req.Extend = phone_number;
                req.SmsType = "normal";
                req.SmsFreeSignName = "身份验证";
                req.SmsParam = "{\"code\":\"" + vercode.ToString() + "\",\"product\":\"一折数码\"}";
                req.RecNum = phone_number;
                req.SmsTemplateCode = "SMS_4935837";
                AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);
                Console.WriteLine(rsp.Body);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}