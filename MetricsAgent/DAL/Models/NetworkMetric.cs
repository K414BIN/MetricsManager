using System;

namespace MetricsAgent.DAL.Models
{
    public class NetworkMetric
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
