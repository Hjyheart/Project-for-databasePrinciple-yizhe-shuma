using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Express
    {
        public Express()
        {

        }

        public Express(TP_EXPRESS tp)
        {
            ID = tp.ID;
            Name = tp.NAME;
            CheckNumber = tp.CHECK_NUMBER;
        }

        public TP_EXPRESS CreateModel()
        {
            TP_EXPRESS tp = new TP_EXPRESS();
            try
            {
                tp.NAME = Name;
                tp.CHECK_NUMBER = CheckNumber;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        public decimal ID { get; set; }

        public string Name { get; set; }

        public string CheckNumber { get; set; }
    }
}