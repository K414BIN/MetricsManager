using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
            ConfigureSqlLiteConnection(services);
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
            services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
        }
        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            string connectionString = "Data Source=:memory:";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
            services.AddSingleton(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS rammetrics";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS hddmetrics";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS networkmetrics";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY, value INT)";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY, value INT)";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY, value INT, time INT64)";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY, value INT, time INT64)";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY, value INT, time INT64)";
                command.ExecuteNonQuery();

                // ��������� ������� CPUMetrics
                command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(10,1)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(50,2)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(75,4)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(90,5)";
                command.ExecuteNonQuery();

                //��������� ������� DotNetMetrics
                command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(10,1)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(50,2)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(75,4)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(90,5)";
                command.ExecuteNonQuery();

                //��������� ������� NetworkMetrics
                command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(10,1)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(50,2)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(75,4)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(90,5)";
                command.ExecuteNonQuery();

                //��������� ������� hddMetrics
                command.CommandText = "INSERT INTO hddmetrics(value) VALUES(10)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(value) VALUES(50)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(value) VALUES(75)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(value) VALUES(90)";
                command.ExecuteNonQuery();

                //��������� ������� RamMetrics
                command.CommandText = "INSERT INTO rammetrics(value) VALUES(10)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(value) VALUES(50)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(value) VALUES(75)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(value) VALUES(90)";
                command.ExecuteNonQuery();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
