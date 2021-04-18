using System;

namespace MetricsManager.Requests
{
   public class GetAllDotNetMetricsApiRequest
    {
        public Uri ClientBaseAddress { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}