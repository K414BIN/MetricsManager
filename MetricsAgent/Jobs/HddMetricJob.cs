using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repositories;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Windows;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class HddMetricJob : IJob
    {
        
        private readonly ILogger<HddMetricJob> _logger;
        private IHddMetricsRepository _repository;
        private PerformanceCounter _hddCounter;


        public HddMetricJob(ILogger<HddMetricJob> logger)
        {
            String perfoCategory = "LogicalDisk";
            _logger = logger;
            _repository = new HddMetricsRepository();
            _logger.LogInformation("Start HddMetricJob");
            PerformanceCounter  performanceCounter = new System.Diagnostics.PerformanceCounter
            {
                MachineName = "DESKTOP-QDKASVN",
                CounterName ="Free Megabytes",
                InstanceName ="C:"
            };
            
            _hddCounter = new PerformanceCounter(perfoCategory,performanceCounter.CounterName , performanceCounter.InstanceName);
            
        }

        public Task Execute(IJobExecutionContext context)
        {

            var hddUsageInPercents = Convert.ToInt32(_hddCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new HddMetric { Value = hddUsageInPercents, Time =time });
            _logger.Log(LogLevel.Information, "Pull job: {0} MB free on HDD on time {1}  sec.",hddUsageInPercents,time);
            return Task.CompletedTask;
        }
    }
}


