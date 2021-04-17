using System;

namespace MetricsManager.Requests
{
   public class GetAllHddMetricsApiRequest
    {
        public Uri ClientBaseAddress{ get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}