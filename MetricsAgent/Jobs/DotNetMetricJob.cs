using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
      
            private readonly IServiceProvider _provider;
            private IDotNetMetricsRepository _repository;
            private PerformanceCounter _DotNetCounter;

            public DotNetMetricJob(IServiceProvider provider)
            {
                _provider = provider;
                _repository = _provider.GetService<IDotNetMetricsRepository>();
                _DotNetCounter = new PerformanceCounter("DotNetGarbageHeap", "% Usage", "_Total");
            }

            public Task Execute(IJobExecutionContext context)
            {
                var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                var DotNet= Convert.ToInt32(_DotNetCounter.NextValue());
                
                _repository.Create(new DotNetMetric { Time = time, Value = DotNet });
                return Task.CompletedTask;
            }
        
 
    }
}