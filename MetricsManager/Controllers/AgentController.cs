using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly Agents _holder;
        private readonly ILogger<AgentsController> _logger;

        public AgentsController(ILogger<AgentsController> logger,Agents holder)
        {
            _logger = logger;
            _holder = holder;
        }


        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _holder.ListAgents.Add(agentInfo);
            _logger.Log(LogLevel.Information, "Registering agent {0} at address {1}",agentInfo.AgentId,agentInfo.AgentAddress);
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _holder.ListActiveAgents.Add(agentId);
            _logger.Log(LogLevel.Information, "Enabling agent {0}",agentId);
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            if (_holder.ListActiveAgents.Contains(agentId))
            {
                _holder.ListActiveAgents.Remove(agentId);
                _logger.Log(LogLevel.Information, "Disabling agent {0}",agentId);
            }
            return Ok();
        }

        [HttpGet("list-agents")]
        public IActionResult ListAgents()
        {
            return Ok(_holder.ListAgents.ToArray());
        }

        [HttpGet("list-active-agents")]
        public IActionResult ListActiveAgenstById()
        {
            return Ok(_holder.ListActiveAgents.ToArray());
        }
    }
}
