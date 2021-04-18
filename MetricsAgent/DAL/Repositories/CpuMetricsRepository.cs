using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Core;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repositories
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        // строка подключения
        private readonly string ConnectionString = SQLSettings.ConnectionString;
        
        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        public CpuMetricsRepository()
        {
            // добавляем парсилку типа TimeSpan в качестве подсказки для SQLite
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
         
        }

        public void Create(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
               // connection.ExecuteScalar("CREATE TABLE IF NOT EXISTS cpumetrics (`id` INTEGER  ,'value' INTEGER, 'time' INT64, PRIMARY KEY(`id`)", null);
                 //connection.Execute(
                 //   @"CREATE TABLE IF NOT EXISTS cpumetrics (`id` INTEGER  ,'value' INTEGER, 'time' INT64, PRIMARY KEY(`id`)", param: null,null,null,null);
                
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)", 
                    // анонимный объект с параметрами запроса
                    new { 
                        // value подставится на место "@value" в строке запроса
                        // значение запишется из поля Value объекта item
                        value = item.Value,
                    
                        // записываем в поле time количество секунд
                        time = item.Time.TotalSeconds 
                    });
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("DELETE FROM cpumetrics WHERE id=@id",
                    new
                    {
                        id = id
                    });
            }
        }

        public void Update(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE cpumetrics SET value = @value, time = @time WHERE id=@id",
                    new
                    {   
                        value = item.Value,
                        time = item.Time.TotalSeconds,
                        id = item.Id
                    });
            }
        }

        public IList<CpuMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                // читаем при помощи Query и в шаблон подставляем тип данных
                // объект которого Dapper сам и заполнит его поля
                // в соответсвии с названиями колонок
                return connection.Query<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics").ToList();
            }
        }

        public CpuMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics WHERE id=@id",
                    new {id = id});
            }
        }
    }
} 

