using System;

namespace MetricsAgent.DAL.Models
{
    public class RamMetric
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}