﻿using MetricsAgent.Requests;
using MetricsAgent.Responses;

namespace MetricsAgent.Client
{
    public interface IMetricsAgentClient
    {
    
            AllRamMetricsApiResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);

            AllHddMetricsApiResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);

           AllDotNetMetricsApiResponse GetAllDotNetMetrics(GetAllDotNetMetricsApiRequest request);

            AllCpuMetricsApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request);
            
            AllNetworkMetricsApiResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request);
        
    }
}