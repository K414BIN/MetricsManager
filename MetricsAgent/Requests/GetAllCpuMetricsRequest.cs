using System;

namespace MetricsAgent.Requests
{
   public class GetAllCpuMetricsRequest
    {
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}