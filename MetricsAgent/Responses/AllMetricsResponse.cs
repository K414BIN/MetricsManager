using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    internal class AllMetricsResponse<T> where T : class
    {
        public List<T> Metrics { get; set; }
    }
}