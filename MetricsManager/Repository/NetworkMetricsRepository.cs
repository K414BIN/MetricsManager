﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using Dapper;
using Core;
using MetricsManager.Models;
using ServiceStack.Text;

namespace MetricsManager.Repository
{
    public class NetworkMetricsRepository 
    {
        // строка подключения
        private readonly string ConnectionString = SQLSettings.ManagerConnectionString;

        private string _tblname = "NetworkMetrics";

        public void Create(NetworkMetrics item)
        {
            
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute($@"CREATE TABLE IF NOT EXISTS  {_tblname} (id INTEGER PRIMARY KEY, value INT, agentid INT,time INT64)");
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute($@"INSERT INTO {_tblname}(value, time, agentid) VALUES(@valueIn, @timeIn, @agentidIn)", 
                    // анонимный объект с параметрами запроса
                    new { 
                        valueIn = item.value,
                        agentidIn=item.agentid,
                        timeIn = item.time
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

        public void Update(NetworkMetrics item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute($"UPDATE {_tblname} SET value = @valueIn, agentid=@agentIn, time = @timeIn WHERE id=@idIn",
                    new
                    {   
                        valueIn = item.value,
                        timeIn = item.time,
                        idIn = item.id,
                        agentIn =item.agentid
                    });
            }
        }
        
        public IList<NetworkMetrics> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<NetworkMetrics>($"SELECT * FROM {_tblname}").ToList();
            }
        }

        public NetworkMetrics GetById(int idIn)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<NetworkMetrics>($"SELECT * FROM {_tblname} WHERE id=@id",
                    new {id = idIn});
            }
        }
               
    }
} 

