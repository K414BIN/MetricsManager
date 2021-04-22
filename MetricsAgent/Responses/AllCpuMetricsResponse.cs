using MetricsAgent.Models;

namespace MetricsAgent.Responses
{
    public class AllCpuMetricsResponse 
    { 
        public long FromTime { get; set; }
        public long ToTime { get; set; }
    }
}
