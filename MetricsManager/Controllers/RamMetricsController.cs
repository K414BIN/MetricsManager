using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {

        [HttpGet("agent/{agentId}/available/memoryinGb/{memoryingb}")]
        public IActionResult GetMetricsAvailabeMemoryFromAgent([FromRoute] int agentId, [FromRoute] MemoryInGB memoryInGB)
        {
            return Ok();
        }

        [HttpGet("available/memoryinGb/{memoryingb}")]
        public IActionResult GetMetricsAvailabeMemory([FromRoute] MemoryInGB memoryInGB)
        {
            return Ok();
        }
    }
}
