using Core.Interfaces;
using MetricsAgent.DAL.Models;

namespace MetricsManager
{
    public interface IAgentsRepository : IAgentRepository<AgentInfo>
    {
    }
}