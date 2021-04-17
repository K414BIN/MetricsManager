using System;

namespace MetricsAgent.Requests
{
   public class GetAllDotNetMetricsRequest
    {
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}