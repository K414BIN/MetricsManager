
using System;

namespace MetricsAgent.Models
{
    public class RamMetricDto
    {
        public int Value { get; set; }
        public int Id { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}