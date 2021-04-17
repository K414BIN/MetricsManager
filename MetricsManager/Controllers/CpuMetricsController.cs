﻿using System;
using Core;
using MetricsManager.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MetricsAgent.Requests;
using System.Net.Http;
using System.Text.Json;

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

        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{Percentile}")]
        public IActionResult GetCpuMetricsByPercentile([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"GetCpuMetricsByPercentile - From time : {fromTime};  To time: { toTime};  Percentile {percentile}");
            return Ok();
        }

        //[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        //public IActionResult GetMetricsCpuFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        //{
        //    _logger.Log(LogLevel.Information, "Requested agent: {0} between time {1} - {2} sec.", agentId.ToString(),fromTime.TotalSeconds, toTime.TotalSeconds);
        //    return Ok();
        //}

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsCpu([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.Log(LogLevel.Information, "Requested between time {0} - {1} sec.",fromTime.TotalSeconds, toTime.TotalSeconds);
            return Ok();
        }

      
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://localhost:50343/api/cpumetrics/from/1/to/999999");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            var client = clientFactory.CreateClient();
            HttpResponseMessage response = client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                using var responseStream =  response.Content.ReadAsStreamAsync().Result;
                var metricsResponse  =  JsonSerializer.DeserializeAsync
                    <CpuMetricsResponse>(responseStream).Result;
            }
            else
            {
                // ошибка при получении ответа
            }
            return Ok();
        }


    }
}
