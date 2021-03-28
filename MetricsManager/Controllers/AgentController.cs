using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly Agents _holder;

        public AgentsController(Agents holder)
        {
            _holder = holder;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _holder.ListAgents.Add(agentInfo);
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _holder.ListActiveAgents.Add(agentId);
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            if (_holder.ListActiveAgents.Contains(agentId))
            {
                _holder.ListActiveAgents.Remove(agentId);
            }
            return Ok();
        }

        [HttpGet("list-agents")]
        public IActionResult ListAgents()
        {
            return Ok(_holder.ListAgents.ToArray());
        }

        [HttpGet("list-activeagents")]
        public IActionResult ListActiveAgenstById()
        {
            return Ok(_holder.ListActiveAgents.ToArray());
        }
    }
}
