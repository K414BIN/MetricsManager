using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Core;
using Dapper;
using MetricsManager.Interfaces;
using MetricsManager;

namespace MetricsManager.Repository
{
    public class AgentsRepository : IAgentsRepository
    {
        private readonly string ConnectionString = SQLSettings.ManagerConnectionString;

        public void Create(AgentInfo item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute(
                    @"CREATE TABLE IF NOT EXISTS agents (`id` INTEGER  , PRIMARY KEY(`id`), agentid INT,agenturl VARCHAR(512) CHARACTER SET 'ascii' COLLATE 'ascii_general_ci' NOT NULL");
                connection.Execute("INSERT INTO agents(agentid,agenturl) VALUES(@agentid,@agenturl)",
                    new
                    {   agentid  = item.AgentId ,
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
        
        public AgentInfo GetLast()
        {
            AgentInfo vAgentInfo;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                vAgentInfo= (AgentInfo)connection.QueryFirst<AgentInfo>("SELECT * FROM agents ORDER BY id DESC LIMIT 1 ");
            }

            return (vAgentInfo);
        
        }

        public AgentInfo GetFirst()
        {
            AgentInfo vAgentInfo;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
               vAgentInfo= (AgentInfo)connection.QueryFirst<AgentInfo>("SELECT * FROM agents ORDER BY id ASC LIMIT 1 ");
            }

            return (vAgentInfo);
        }

        public void Delete(int agentId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("DELETE FROM agents WHERE agentid=@agentId", agentId);
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

        public void Update(AgentInfo item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE agents SET agenturl = @newurl, agentid = @agentId WHERE id=@id",
                    new
                    {
                        agentid = item.AgentId,
                        newurl= item.AgentAddress,
                        id = item.Id
                    });
            }
        }
    }
}
