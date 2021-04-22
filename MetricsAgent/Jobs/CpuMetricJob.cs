using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class CpuMetricJob : IJob
    {
      
        private ICpuMetricsRepository _repository;
        private readonly ILogger<CpuMetricJob> _logger;
      
        // счетчик для метрики CPU
        private PerformanceCounter _cpuCounter;

        public CpuMetricJob(ILogger<CpuMetricJob> logger)
        {
            _logger = logger;
            _repository = new CpuMetricsRepository();
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _logger.LogInformation("Start CpuMetricJob");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());
            
            // узнаем когда мы сняли значение метрики.
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new CpuMetric { Time = time, Value = cpuUsageInPercents });
            _logger.Log(LogLevel.Information, "Pull job: {0} % CPU usage on time {1}  sec.",cpuUsageInPercents,time);
            
            return Task.CompletedTask;
        }
    }
}