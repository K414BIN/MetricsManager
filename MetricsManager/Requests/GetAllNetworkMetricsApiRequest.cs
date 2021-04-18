using System;

namespace MetricsManager.Requests
{
   public class GetAllNetworkMetricsApiRequest
    {
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public Uri   ClientBaseAddress{ get; set; }
    }
}