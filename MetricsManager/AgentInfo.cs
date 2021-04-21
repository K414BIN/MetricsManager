using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsManager
{
    public class AgentInfo
    {
        

        public Uri AgentAddress { get; set; }
        public int Id { get; set; }
        public int AgentId  { get; set; }
    
    }
}