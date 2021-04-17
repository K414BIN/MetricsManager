using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Repositories;
using MetricsAgent.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
            services.AddHttpClient(); 
            services.AddTransient<INotifierMediatorService, NotifierMediatorService>();
//services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>().AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(5000)));
            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<ICpuMetricsRepository,CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository,DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository,HddMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(CpuMetricJob),
                cronExpression: "0/5 * * * * ?"));
            ////****************************************************
            //services.AddSingleton<NetworkMetricJob>();
            //services.AddSingleton(new JobSchedule(
            //    jobType: typeof(NetworkMetricJob),
            //    cronExpression: "0/5 * * * * ?"));
            ////****************************************************
            //services.AddSingleton<DotNetMetricJob>();
            //services.AddSingleton(new JobSchedule(
            //    jobType: typeof(DotNetMetricJob),
            //    cronExpression: "0/5 * * * * ?"));
            ////****************************************************
            //services.AddSingleton<HddMetricJob>();
            //services.AddSingleton(new JobSchedule(
            //    jobType: typeof(HddMetricJob),
            //    cronExpression: "0/5 * * * * ?"));
            ////****************************************************
            //services.AddSingleton<RamMetricJob>();
            //services.AddSingleton(new JobSchedule(
            //    jobType: typeof(RamMetricJob),
            //    cronExpression: "0/5 * * * * ?"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

    //        app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
