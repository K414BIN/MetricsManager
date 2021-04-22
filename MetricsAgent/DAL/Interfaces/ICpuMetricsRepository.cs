using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using Core;
using Core.Interfaces;
using Dapper;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Interfaces
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {
    
        public static string UrlEncode( DateTimeOffset dateTimeOffset)
        {
            return HttpUtility.UrlEncode(dateTimeOffset.ToString("o"));
        }

        List<CpuMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            long ftime = Convert.ToInt64(UrlEncode(fromTime));
            long ttime = Convert.ToInt64(UrlEncode(toTime));
            using (var connection = new SQLiteConnection(SQLSettings.ConnectionString))
            {
                return connection.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE time>@fromTime AND time<@toTime",
                    new {fromTime = ftime, toTime = ttime}).ToList();
            }
        }
    }
}