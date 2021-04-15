using System;

namespace MetricsAgent.Requests
{
   public class GetAllRamMetricsApiRequest
    {
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public Uri AgentAddress { get; set; }
    }
}