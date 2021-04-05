using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Models
{
    public class DotNetMetric
    {
        public int Id { get; set; }

        public TimeSpan Time { get; set; }

        public int ErrorsCount { get; set; }
    }
}
