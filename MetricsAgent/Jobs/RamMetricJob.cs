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
    public class RamMetricJob: IJob
    {
           // Инжектируем DI провайдер
            private readonly IServiceProvider _provider;
            private IRamMetricsRepository _repository;
        
        
            private PerformanceCounter _ramCounter;
            

            public RamMetricJob(IServiceProvider provider)
            {
                _provider = provider;
                _repository = _provider.GetService<IRamMetricsRepository>();
                _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            }

            public Task Execute(IJobExecutionContext context)
            {
 
                var ramUsageInPercents = Convert.ToInt32(_ramCounter.NextValue());

                // узнаем когда мы сняли значение метрики.
                var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

                _repository.Create(new RamMetric { Value= ramUsageInPercents });
            
                return Task.CompletedTask;
            }
        }
    }
 

