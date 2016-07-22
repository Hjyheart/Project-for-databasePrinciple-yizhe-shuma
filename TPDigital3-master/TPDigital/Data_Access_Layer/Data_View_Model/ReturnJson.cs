using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class ReturnJson
    {
        public string currentPage;
        public string totalPages;
        public string data;

        public ReturnJson() { }
        public ReturnJson(string _currentPage,string _totalPages,string _data)
        {
            currentPage = _currentPage;
            totalPages = _totalPages;
            data = _data;
        }
    }
}