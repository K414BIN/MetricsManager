using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MainLibrary;
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

        [HttpGet("agent/{agentId}/left/memoryinGb/{memoryingb}")]
        public IActionResult GetMetricsFreeLeftMemoryFromAgent([FromRoute] int agentId, [FromRoute] MemoryInGb memoryInGB)
        {
            _logger.LogInformation("Requested agent: {0} {1} memory in Gb", agentId, memoryInGB);
            return Ok();
        }

        [HttpGet("left/memoryinGb/{memoryingb}")]
        public IActionResult GetMetricsFreeLeftMemory([FromRoute] MemoryInGb memoryInGB)
        {   _logger.LogInformation("Requested {0} memory in GB", memoryInGB);
            return Ok();
        }
    }
}
