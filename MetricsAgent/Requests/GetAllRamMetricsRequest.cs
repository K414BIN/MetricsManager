using System;

namespace MetricsAgent.Requests
{
   public class GetAllRamMetricsRequest
    {
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}