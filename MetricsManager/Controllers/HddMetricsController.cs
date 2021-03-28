using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        [HttpGet("agent/{agentId}/left/memoryinGb/{memoryingb}")]
        public IActionResult GetMetricsFreeLeftMemoryFromAgent([FromRoute] int agentId, [FromRoute] MemoryInGB memoryInGB)
        {
            return Ok();
        }

        [HttpGet("left/memoryinGb/{memoryingb}")]
        public IActionResult GetMetricsFreeLeftMemory([FromRoute] MemoryInGB memoryInGB)
        {
            return Ok();
        }
    }
}
