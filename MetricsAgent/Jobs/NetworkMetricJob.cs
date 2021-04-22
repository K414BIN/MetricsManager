using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repositories;
using Microsoft.Extensions.Logging;
using Quartz;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class NetworkMetricJob : IJob
    {
        private readonly ILogger<NetworkMetricJob> _logger;
        private INetworkMetricsRepository _repository;
        private PerformanceCounter _netCounter;

        public NetworkMetricJob(ILogger<NetworkMetricJob> logger)
        {
            String perfoCategory = "Network Interface";
            _logger = logger;
            _logger.LogInformation("Start NetworkMetricJob");
          
            _repository = new NetworkMetricsRepository();
            PerformanceCounterCategory category = new PerformanceCounterCategory(perfoCategory);
            string[] instancename = category.GetInstanceNames();

            PerformanceCounter performanceCounter= new System.Diagnostics.PerformanceCounter
            {
                CategoryName= perfoCategory,
                CounterName = "Bytes Received/sec",
                InstanceName = instancename[0],
                MachineName = "DESKTOP-QDKASVN"
                
            };
            
            if (instancename.Count() > 0)
            {
                _netCounter = new PerformanceCounter(perfoCategory,performanceCounter.CounterName , performanceCounter.InstanceName);
            }
            else
            {
                throw new System.InvalidOperationException("Instance category does not exist!");
            }
        }

        public Task Execute(IJobExecutionContext context)
        {
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            var netSpeed = Convert.ToInt32(_netCounter.NextValue());
            _repository.Create(new NetworkMetric { Time = time, Value = netSpeed });
            _logger.Log(LogLevel.Information, "Pull job: {0}  Bytes received per second on time {1}  sec.",_netCounter,time);
            return Task.CompletedTask;
        }
    }
}


