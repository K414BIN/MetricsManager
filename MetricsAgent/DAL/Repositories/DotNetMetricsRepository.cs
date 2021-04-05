using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using MainLibrary;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Interfaces;

namespace MetricsAgent.DAL.Repositories
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private SQLiteConnection _connection;

        public DotNetMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }
        public void Create(DotNetMetric item)
        {
            // создаем команду
            using var cmd = new SQLiteCommand(_connection);
            // создаем таблицу, если ее нет 
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS  dotnetmetrics   (
                                                                              `id` INTEGER  ,
                                                                              `value` INT, `time` INT64,
                                                                               PRIMARY KEY(`id`)
                                                                          );";
            cmd.ExecuteNonQuery();
        
            // прописываем в команду SQL запрос на вставку данных
            cmd.CommandText = "INSERT INTO  dotnetmetrics(value, time) VALUES(@value, @time)";
            // добавляем параметры в запрос из нашего объекта
            cmd.Parameters.AddWithValue("@value",item.ErrorsCount);
            cmd.Prepare();

            // выполнение команды
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var cmd = new SQLiteCommand(_connection);
            // прописываем в команду SQL запрос на удаление данных
            cmd.CommandText = "DELETE FROM dotnetmetrics WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Update(DotNetMetric item)
        {
            using var cmd = new SQLiteCommand(_connection);
            // прописываем в команду SQL запрос на обновление данных
            cmd.CommandText = "UPDATE dotnetmetrics SET value = @value WHERE id=@id;";
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Parameters.AddWithValue("@value", item.ErrorsCount);
            cmd.Parameters.AddWithValue("@time", item.Time.TotalSeconds);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<DotNetMetric> GetAll()
        {
            using var cmd = new SQLiteCommand(_connection);

            // прописываем в команду SQL запрос на получение всех данных из таблицы
            cmd.CommandText = "SELECT * FROM dotnetmetrics";

            var returnList = new List<DotNetMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new DotNetMetric
                    {
                        Time = TimeSpan.FromSeconds(reader.GetInt32(0)),
                        Id = reader.GetInt32(0),
                        ErrorsCount = reader.GetInt32(0)
                    });
                }
            }
            return returnList;
        }

        public DotNetMetric GetById(int id)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = "SELECT * FROM dotnetmetrics WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // если удалось что то прочитать
                if (reader.Read())
                {
                    // возвращаем прочитанное
                    return new DotNetMetric
                    {
                        Id = reader.GetInt32(0),
                        Time = TimeSpan.FromSeconds(reader.GetInt32(0)),
                        ErrorsCount = reader.GetInt32(0)
                    };
                }
                else
                {
                    // не нашлось запись по идентификатору, не делаем ничего
                    return null;
                }
            }
        }
    }
}

