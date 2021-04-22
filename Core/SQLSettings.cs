﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Core
{
   public  class SQLSettings
   {
        public static  string ConnectionString = @"Data Source=metrics.db; Version=3;Pooling=True;Max Pool Size=100;";
        public static  string ManagerConnectionString = @"Data Source=metricsmanager.db; Version=3;Pooling=True;Max Pool Size=100;";
             
        public static string UrlEncode( DateTimeOffset dateTimeOffset)
        {
            return HttpUtility.UrlEncode(dateTimeOffset.ToString("o"));
        }
    }
}
