﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MainLibrary;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Repositories;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 

    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;

        public HddMetricsController(ILogger<HddMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");
        }
        private HddMetricsRepository _repository;

        public HddMetricsController(IHddMetricsRepository repository)
        {
            _repository = (HddMetricsRepository)repository;
        }

        [HttpGet("left/memoryinGb/{memoryingb}")]
        public IActionResult GetMetricsFreeLeftMemory([FromRoute] MemoryInGb memoryInGb)
        {
            return Ok();
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _repository.Create(new HddMetric
            {
                FreeMemorySizeInGb = request.Value
               
            });

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetric>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetric { FreeMemorySizeInGb = metric.FreeMemorySizeInGb, Id = metric.Id });
            }

            return Ok(response);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            _repository.Delete(id);
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] HddMetricUpdateRequest request)
        {
            // что-то пошло не так это надо доделать
            var result = new HddMetric { FreeMemorySizeInGb = request.Value };
            var response = _repository.GetById(request.Id);

            _repository.Update(result);

            return Ok();
        }

        [HttpGet("getbyid")]
        public IActionResult GetById([FromRoute] int id)
        {
            _repository.GetById(id);
            return Ok();
        }

    }
}
