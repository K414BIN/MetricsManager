using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MainLibrary;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {

        private readonly ILogger<RamMetricsController> _logger;

        public RamMetricsController(ILogger<RamMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");
        }

   

        [HttpGet("agent/{agentId}/available/memoryinGb/{memoryingb}")]
        public IActionResult GetMetricsAvailabeMemoryFromAgent([FromRoute] int agentId, [FromRoute] MemoryInGb memoryInGB)
        {
        
           _logger.Log(LogLevel.Information, "Requested agent: {0} available {1} memory in Gb.",agentId, memoryInGB);
            return Ok();
        }

        [HttpGet("available/memoryinGb/{memoryingb}")]
        public IActionResult GetMetricsAvailabeMemory([FromRoute] MemoryInGb memoryInGB)
        {
            _logger.Log(LogLevel.Information, "Requested available {0} memory in Gb.", memoryInGB);
            return Ok();
        }
    }
}
