using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Core;
using MetricsManager.Client;
using MetricsManager.Interfaces;
using MetricsManager.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using FluentMigrator.Runner;

namespace MetricsManager
{
    public class Startup
    {
        private readonly string _connectionString = SQLSettings.ManagerConnectionString;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        { 
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            services.AddSingleton(_connectionString);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton<IAgentsRepository, AgentsRepository>();
            ConfigureSqlLiteConnection(services);
            services.AddHttpClient();
           // services.AddControllers();
            //_ = services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>().AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(5000)));
            services.AddFluentMigratorCore()
              .ConfigureRunner(rb => rb
                  // добавляем поддержку SQLite 
                  .AddSQLite()
                  // устанавливаем строку подключения
                  .WithGlobalConnectionString(_connectionString)
      
                  // подсказываем где искать классы с миграциями
                  .ScanIn(typeof(Startup).Assembly).For.Migrations()
              ).AddLogging(lb => lb
                  .AddFluentMigratorConsole());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
        //    app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            migrationRunner.MigrateUp();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
