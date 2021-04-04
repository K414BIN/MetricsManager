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
        public class RamMetricsRepository : IRamMetricsRepository
        {
            private readonly SQLiteConnection _connection;
       
            public RamMetricsRepository(SQLiteConnection connection)
            {
                _connection = connection;
            }

            public void Create(RamMetric item)
            {
                // создаем команду
                using var cmd = new SQLiteCommand(_connection);
                // создаем таблицу, если ее нет 
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS rammetrics   (
                                                                        `id` int(11)  ,
                                                                         `value` int, 
                                                                         PRIMARY KEY(`id`)
                                                                          );";
                cmd.ExecuteNonQuery();

                // прописываем в команду SQL запрос на вставку данных
                cmd.CommandText = "INSERT INTO rammetrics(value) VALUES(@value)";

                // добавляем параметры в запрос из нашего объекта
                cmd.Parameters.AddWithValue("@value", item.AvailableMemorySizeInGb);
                cmd.Prepare();

                // выполнение команды
                cmd.ExecuteNonQuery();
            }

            public void Delete(int id)
            {
                using var cmd = new SQLiteCommand(_connection);
                // прописываем в команду SQL запрос на удаление данных
                cmd.CommandText = "DELETE FROM rammetrics WHERE id=@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }

            public void Update(RamMetric item)
            {
                using var cmd = new SQLiteCommand(_connection);
                // прописываем в команду SQL запрос на обновление данных
                cmd.CommandText = "UPDATE rammetrics SET value = @value WHERE id=@id;";
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@value", item.AvailableMemorySizeInGb);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }

            public IList<RamMetric> GetAll()
            {
                using var cmd = new SQLiteCommand(_connection);

                // прописываем в команду SQL запрос на получение всех данных из таблицы
                cmd.CommandText = "SELECT * FROM rammetrics";

                var returnList = new List<RamMetric>();

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    // пока есть что читать -- читаем
                    while (reader.Read())
                    {
                        // добавляем объект в список возврата
                        returnList.Add(new RamMetric
                        {
                            Id = reader.GetInt32(0),
                            AvailableMemorySizeInGb = reader.GetInt32(0),
                        });
                    }
                }
                return returnList;
            }

            public RamMetric GetById(int id)
            {
                using var cmd = new SQLiteCommand(_connection);
                cmd.CommandText = "SELECT * FROM rammetrics WHERE id=@id";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    // если удалось что то прочитать
                    if (reader.Read())
                    {
                        // возвращаем прочитанное
                        return new RamMetric
                        {
                            Id = reader.GetInt32(0),
                            AvailableMemorySizeInGb = reader.GetInt32(0)
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

