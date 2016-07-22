using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using TPDigital.Models;

namespace TPDigital.Data_Access_Layer.Data_Access_Layer
{
    public class DBConn
    {
        //public static OracleDbContext db = new OracleDbContext();
        public static OracleDbContext createDbContext()
        {
            OracleDbContext db = (OracleDbContext)CallContext.GetData("OracleDbContext");
            if (db == null)
            {
                db = new OracleDbContext();
                CallContext.SetData("OracleDbContext", db);
            }
            return db;
        }


    }
}