﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Core;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ICpuMetricsRepository _repository;

        private readonly ILogger<CpuMetricsController> _logger;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, ICpuMetricsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("from/{fromTime}/to/{toTime}/")]
        public IActionResult GetCpuMetricsTimeInterval([FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"GetCpuMetricsTimeInterval - From time: {fromTime}; To time: {toTime}");
            List<CpuMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            var response = new AllMetricsResponse<CpuMetricDto>()
            {
                Metrics = new List<CpuMetricDto>()

            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }

            return Ok(response);
        }




        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{Percentile}")]
        public IActionResult GetCpuMetricsByPercentileTimeInterval([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"GetCpuMetricsByPercentileTimeInterval - From time : {fromTime};  To time: { toTime};  Percentile {percentile}");

            return Ok();
        }

    }
}
