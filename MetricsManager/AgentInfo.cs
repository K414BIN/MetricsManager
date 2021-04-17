using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsManager
{
    public class AgentInfo
    {
        public AgentInfo(int agentId, Uri agentAddress)
        {
            AgentId = agentId;
            AgentAddress = agentAddress;
        }

        public Uri AgentAddress { get;}

        public int AgentId  { get;}
    
    }
}