using System;

namespace MetricsAgent.DAL.Models
{
    public class CpuMetric
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}