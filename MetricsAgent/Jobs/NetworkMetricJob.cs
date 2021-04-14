using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Quartz;

namespace MetricsAgent.Jobs
{
        public class NetworkMetricJob : IJob
        {
            // Инжектируем DI провайдер
            private readonly IServiceProvider _provider;
            private INetworkMetricsRepository repository;
            private PerformanceCounter _netCounter;

            public NetworkMetricJob(IServiceProvider provider)
            {
                _provider = provider;
                repository = _provider.GetService<INetworkMetricsRepository>();
                _netCounter = new PerformanceCounter("Network", "MBytes per second");
            }

            public Task Execute(IJobExecutionContext context)
            {
                var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                var netSpeed = Convert.ToInt32(_netCounter.NextValue());
                  repository.Create(new NetworkMetric { Time = time, Value = netSpeed });
                return Task.CompletedTask;
            }
        }
}


