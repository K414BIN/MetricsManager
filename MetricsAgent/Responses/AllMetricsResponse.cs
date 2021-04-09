using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Responses
{
    public class AllMetricsResponse< M >  where M : class
    {
        public List< M > Metrics { get; set; }
    }
}
