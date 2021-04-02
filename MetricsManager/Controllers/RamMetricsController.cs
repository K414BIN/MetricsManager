using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MainLibrary;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {

        [HttpGet("agent/{agentId}/available/memoryinGb/{memoryingb}")]
        public IActionResult GetMetricsAvailabeMemoryFromAgent([FromRoute] int agentId, [FromRoute] MemoryInGb memoryInGB)
        {
            return Ok();
        }

        [HttpGet("available/memoryinGb/{memoryingb}")]
        public IActionResult GetMetricsAvailabeMemory([FromRoute] MemoryInGb memoryInGB)
        {
            return Ok();
        }
    }
}
