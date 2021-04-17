using System;

namespace MetricsManager.Requests
{
   public class GetAllRamMetricsApiRequest
    {
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public Uri AgentAddress { get; set; }
    }
}