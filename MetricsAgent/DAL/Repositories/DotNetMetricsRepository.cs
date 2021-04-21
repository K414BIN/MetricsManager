using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Core;
using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Repositories
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        // строка подключения
        private readonly string ConnectionString = SQLSettings.ConnectionString;
        
        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        public DotNetMetricsRepository()
        {
            // добавляем парсилку типа TimeSpan в качестве подсказки для SQLite
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        public void Create(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute(@"CREATE TABLE IF NOT EXISTS  dotnetmetrics (id INTEGER PRIMARY KEY, value INT, time INT64)");
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)", 
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
                connection.Execute("DELETE FROM dotnetmetrics WHERE id=@id",
                    new
                    {
                        id = id
                    });
            }
        }

        public void Update(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE dotnetmetrics SET value = @value, time = @time WHERE id=@id",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds,
                        id = item.Id
                    });
            }
        }

        public IList<DotNetMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                // читаем при помощи Query и в шаблон подставляем тип данных
                // объект которого Dapper сам и заполнит его поля
                // в соответсвии с названиями колонок
                return connection.Query<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics").ToList();
            }
        }

        public DotNetMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics WHERE id=@id",
                    new {id = id});
            }
        }
    }
} 

