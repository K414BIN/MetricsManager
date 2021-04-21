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
    public class DotNetMetricJob : IJob
    {

        private readonly ILogger<DotNetMetricJob> _logger;
        private IDotNetMetricsRepository _repository;
        private PerformanceCounter _DotNetCounter;


        public DotNetMetricJob(ILogger<DotNetMetricJob> logger)
        {
            _repository = new DotNetMetricsRepository();
            _logger = logger;
            _logger.LogInformation("Start DotNetMetricJob");
            _DotNetCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all Heaps", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {

            var DotNet = Convert.ToInt32(_DotNetCounter.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _repository.Create(new DotNetMetric {Time = time, Value = DotNet});
            _logger.Log(LogLevel.Information, "Pull job: {0} # bytes in all Heaps on time {1}  sec.",DotNet,time);
            return Task.CompletedTask;
        }


    }
}