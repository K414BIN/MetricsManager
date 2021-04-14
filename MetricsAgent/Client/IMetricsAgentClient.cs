using MetricsAgent.Responses;

namespace MetricsAgent.Client
{
    public interface IMetricsAgentClient
    {
    
            AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);

            AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);

            DonNetMetricsApiResponse GetDonNetMetrics(DonNetHeapMetrisApiRequest request);

            AllCpuMetricsApiResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request);
            
            AllNetworkMetricsApiResponse GetCpuMetrics(GetAllNetworkMetricsApiRequest request);
        
    }
}