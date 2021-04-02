using System;

namespace MetricsAgent.Requests
{
    public class NetworkMetricUpdateRequest
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}