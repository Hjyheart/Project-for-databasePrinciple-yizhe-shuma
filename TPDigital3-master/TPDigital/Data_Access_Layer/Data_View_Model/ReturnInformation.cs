using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class ReturnInformation
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }

        public ReturnInformation()
        {

        }
        public ReturnInformation(string code, string message = null, string data = null)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }
}