using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {

        private readonly ILogger<CpuMetricsController> _logger;

        public CpuMetricsController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }


        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsCpuFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.Log(LogLevel.Information, "Requested agent: {0} between time {1} - {2} sec.", agentId.ToString(),fromTime.TotalSeconds.ToString(), toTime.TotalSeconds.ToString());
            
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsCpu([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.Log(LogLevel.Information, "Requested between time {0} - {1} sec.",fromTime.TotalSeconds, toTime.TotalSeconds);
            return Ok();
        }
    }
}
