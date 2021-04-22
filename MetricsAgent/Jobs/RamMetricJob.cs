using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repositories;
using Quartz;
using Microsoft.Extensions.Logging;
using Core.Interfaces;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class RamMetricJob : IJob
    {
        private readonly ILogger<RamMetricJob> _logger;
        private IRamMetricsRepository _repository;

        private PerformanceCounter _ramCounter;

        public RamMetricJob(ILogger<RamMetricJob> logger)
        {
            _repository = new RamMetricsRepository();
            String perfoCategory = "Memory";
            _logger = logger;
            _logger.LogInformation("Start RamMetricJob");

            PerformanceCounter performanceCounter= new System.Diagnostics.PerformanceCounter
            {
                CategoryName= perfoCategory,
                CounterName = "Available MBytes",
                MachineName = "DESKTOP-QDKASVN"

            };
                _ramCounter = new PerformanceCounter(perfoCategory,performanceCounter.CounterName , performanceCounter.InstanceName);
        }

        public Task Execute(IJobExecutionContext context)
        {

            var ramUsageInPercents = Convert.ToInt32(_ramCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _repository.Create(new RamMetric { Time = time, Value = ramUsageInPercents });
            _logger.Log(LogLevel.Information, "Pull job: {0} Mb Available in RAM on time {1}  sec.",ramUsageInPercents,time);
            return Task.CompletedTask;
        }
    }
}


