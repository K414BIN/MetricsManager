﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;

      
        public NetworkMetricsController(ILogger<NetworkMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsNetwork([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.Log(LogLevel.Information, "Requested between time {0} - {1} sec.",fromTime.TotalSeconds, toTime.TotalSeconds);
            return Ok();
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsNetworkFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.Log(LogLevel.Information, "Requested agent: {0} between time {1} - {2} sec.",agentId, fromTime.TotalSeconds, toTime.TotalSeconds);
               return Ok();
        }
    }
}
