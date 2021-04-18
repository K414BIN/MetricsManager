using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

   

        [HttpGet("agent/{agentId}/available/{memoryingb}")]
        public IActionResult GetMetricsAvailabeMemoryFromAgent([FromRoute] int agentId, [FromRoute] int memoryInGB)
        {
        
           _logger.Log(LogLevel.Information, "Requested agent: {0} available {1} Gb RAM memory",agentId, memoryInGB);
            return Ok();
        }

        [HttpGet("available/{memoryingb}")]
        public IActionResult GetMetricsAvailabeMemory([FromRoute] int memoryInGB)
        {
            _logger.Log(LogLevel.Information, "Requested available {0} Gb RAM memory", memoryInGB);
            return Ok();
        }
    }
}
