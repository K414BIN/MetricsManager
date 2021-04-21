using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
{
    public class ApiMetric
    {     
        public int id { get; set; }
        public int agentid { get; set; }
        public int value { get; set; }
        public long time { get; set; }
    }
}
