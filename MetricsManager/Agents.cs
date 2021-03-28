using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
{
    public class Agents
    {
        public List<int> ListActiveAgents { get; set; }
        public List<AgentInfo> ListAgents { get; set; }

        public Agents()
        {
            ListAgents = new List<AgentInfo>();
            ListActiveAgents = new List<int>();
        }
    }
}
