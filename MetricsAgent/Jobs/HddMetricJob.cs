//using System;
//using System.Diagnostics;
//using System.Threading.Tasks;
//using MetricsAgent.DAL.Interfaces;
//using MetricsAgent.DAL.Models;
//using Quartz;

//namespace MetricsAgent.Jobs
//{
//    [DisallowConcurrentExecution]
//    public class HddMetricJob: IJob
//    {
//           // Инжектируем DI провайдер
//            private readonly IServiceProvider _provider;
//            private IHddMetricsRepository _repository;
        
        
//            private PerformanceCounter _memCounter;
            

//            public HddMetricJob(IServiceProvider provider)
//            {
//                _provider = provider;
//      //          _repository = _provider.GetService<IHddMetricsRepository>();
//                _memCounter = new PerformanceCounter("Memory", "Available MBytes");
//            }

//            public Task Execute(IJobExecutionContext context)
//            {
 
//                var hddUsageInPercents = Convert.ToInt32(_memCounter.NextValue());

//                // узнаем когда мы сняли значение метрики.
//                var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

//                _repository.Create(new HddMetric { Value= hddUsageInPercents });
            
//                return Task.CompletedTask;
//            }
//        }
//    }
 

