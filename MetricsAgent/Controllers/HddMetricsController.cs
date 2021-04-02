using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MainLibrary;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class HddMetricsController : ControllerBase
    {
        [HttpGet("left/memoryinGb/{memoryingb}")]
        public IActionResult GetMetricsFreeLeftMemory([FromRoute] MemoryInGb memoryInGb)
        {
            return Ok();
        }
    }
}
