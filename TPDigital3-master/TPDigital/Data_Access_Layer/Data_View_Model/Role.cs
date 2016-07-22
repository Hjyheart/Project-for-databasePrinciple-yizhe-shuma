using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Role
    {
        public Role()
        {

        }

        public Role(TP_ROLE tp)
        {
            ID = tp.ID;
            Name = tp.NAME;
        }

        public TP_ROLE CreateModel()
        {
            TP_ROLE tp = new TP_ROLE();
            try
            {
                tp.NAME = Name;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        public decimal ID { get; set; }

        public string Name { get; set; }
    }
}