using System;

namespace MetricsAgent.Requests
{
   public class GetAllHddMetricsRequest
    {
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}