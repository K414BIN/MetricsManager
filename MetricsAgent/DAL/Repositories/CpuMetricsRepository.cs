using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Migrations;
using MetricsAgent.DAL.Models;
using Core;

namespace MetricsAgent.DAL.Repositories
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        // строка подключения
        private readonly string ConnectionString = SQLSettings.ConnectionString;

        private string _tblname = "cpumetrics";

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
                connection.Execute($@"CREATE TABLE IF NOT EXISTS  {_tblname} (id INTEGER PRIMARY KEY, value INT, time INT64)");
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute($"INSERT INTO {_tblname}(value, time) VALUES(@value, @time)", 
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

        public void Delete(int idIn)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute($"DELETE FROM {_tblname} WHERE id=@id",
                    new
                    {
                        id = idIn
                    });
            }
        }

        public void Update(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute($"UPDATE {_tblname} SET value = @value, time = @time WHERE id=@id",
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
                return connection.Query<CpuMetric>($"SELECT * FROM {_tblname}").ToList();
            }
        }

        public CpuMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<CpuMetric>($"SELECT * FROM {_tblname} WHERE id=@id",
                    new {id = id});
            }
        }
               
        public List<CpuMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            long ftime = Convert.ToInt64(SQLSettings.UrlEncode(fromTime));
            long ttime = Convert.ToInt64(SQLSettings.UrlEncode(toTime));
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<CpuMetric>($"SELECT * FROM {_tblname}  WHERE time>@fromTime AND time<@toTime",
                    new {fromTime = ftime, toTime = ttime}).ToList();
            }
        }
    }
} 

