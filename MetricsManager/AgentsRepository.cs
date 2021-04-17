using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Core;
using Core.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace MetricsManager
{
    public class AgentsRepository : IAgentRepository<AgentInfo>
    {
        private string ConnectionString = SQLSettings.ConnectionString;

        public void Create(AgentInfo item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute(
                    @"CREATE TABLE IF NOT EXISTS agents (`id` INTEGER  , PRIMARY KEY(`id`), agenturl VARCHAR(512) CHARACTER SET 'ascii' COLLATE 'ascii_general_ci' NOT NULL");
                connection.Execute("INSERT INTO agents(agenturl) VALUES(@agenturl)",
                    new
                    {
                        agenturl = item.AgentAddress
                    });
            }
        }

        public IList<AgentInfo> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<AgentInfo>("SELECT * FROM agents").ToList();
            }
        }
        
        public IList<AgentInfo> GetLast()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<AgentInfo>("SELECT * FROM agents ORDER BY id DESC LIMIT 1 ").ToList();
            }
        }

        public IList<AgentInfo> GetFirst()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<AgentInfo>("SELECT * FROM agents ORDER BY id ASC LIMIT 1 ").ToList();
            }
        }

        public void Delete(int agentId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("DELETE FROM agents WHERE id=@agentId", agentId);

            }
        }

        public AgentInfo GetAgent(int agentId)
        {
            var agent = new List<AgentInfo> ( GetAll());
            foreach (var value in agent)
            {
                if (value.AgentId == agentId)
                {
                    return (value);
                }
                
            }
            return null;
        }

        public void Update (AgentInfo item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE FROM agents SET agenturl=@newurl WHERE id=@agentId",
                    new {newurl = item.AgentAddress, agentId = item.AgentId});
            }
        }
    }
}
