using System;

namespace MetricsAgent.Requests
{
   public class GetAllNetworkMetricsRequest
    {
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}