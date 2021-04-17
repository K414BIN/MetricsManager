using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentRepositoryAgentsRepository _repository;
        public AgentsController(IHttpClientFactory httpClientFactory)
        {
            httpClientFactory.CreateClient();
        }

        //public test
        //{
        //    return new RestClient(connectionString, _httpClientFactory.CreateClient());
        //}

        public bool TestAgentInDb(int id, Func<(AgentInfo, bool)> checkAgentInfo)
        {
            var agent = _repository.GetAgent(id);
            return checkAgentInfo(agent);
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            TestAgentInDb(agentInfo.AgentId, agentInfo => agentInfo.AgentAddress != null);
            var list = new List<int>();
            list.RemoveAll(item => item == 1);
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            var list = new List<int>();
            list.Add( agentId);
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            
            var list = new List<int>();
            list.RemoveAll(item => item == agentId);

            return Ok();
        }
    }
}
