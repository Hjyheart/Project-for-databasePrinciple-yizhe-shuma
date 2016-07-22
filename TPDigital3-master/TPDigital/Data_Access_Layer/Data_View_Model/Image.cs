using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPDigital.Models;
using TPDigital.Data_Access_Layer.Data_Access_Layer;

namespace TPDigital.Data_Access_Layer.Data_View_Model
{
    public class Image
    {
        public Image()
        {

        }

        public Image(TP_IMAGE tpImage)
        {
            ID = tpImage.ID;
            URL = tpImage.URL;
        }

        public TP_IMAGE CreateModel()
        {
            TP_IMAGE tp = new TP_IMAGE();
            try
            {
                tp.URL = URL;
            }
            catch
            {
                return null;
            }
            return tp;
        }

        public decimal ID { get; set; }

        public string URL { get; set; }
    }
}