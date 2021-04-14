using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {

        private readonly ILogger<HddMetricsController> _logger;

        public HddMetricsController(ILogger<HddMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");
        }

        [HttpGet("agent/{agentId}/left/{memoryingb}")]
        public IActionResult GetMetricsFreeLeftMemoryFromAgent([FromRoute] int agentId, [FromRoute] int memoryInGB)
        {
            _logger.LogInformation("Requested agent: {0} {1} Gb left memory", agentId, memoryInGB);
            return Ok();
        }

        [HttpGet("left/{memoryingb}")]
        public IActionResult GetMetricsFreeLeftMemory([FromRoute] int memoryInGB)
        {  
            _logger.LogInformation("Requested left memory {0} GB", memoryInGB);
            return Ok();
        }
    }
}
