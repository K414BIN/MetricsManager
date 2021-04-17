using System;

namespace MetricsManager.Requests
{
   public class GetAllCpuMetricsApiRequest
    {
        public Uri ClientBaseAddress { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}