using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainLibrary;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;

        public RamMetricsController(ILogger<RamMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");
        }
        private IRamMetricsRepository _repository;

        public RamMetricsController(IRamMetricsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("available/memoryinGb/{memoryingb}")]
        public IActionResult GetMetricsAvailabeMemory([FromRoute] MemoryInGb memoryInGB)
        {
            return Ok();
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _repository.Create(new RamMetric
            {
                AvailableMemorySizeInGb  = request.Value
            });

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetric>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetric { AvailableMemorySizeInGb = metric.AvailableMemorySizeInGb, Id = metric.Id });
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
        public IActionResult Update([FromBody] RamMetricUpdateRequest request)
        {
            // что-то пошло не так это надо доделать
            var result = new RamMetric { AvailableMemorySizeInGb = request.Value };
            var response = _repository.GetById(request.Id);

            _repository.Update(result);

            return Ok();
        }

        [HttpGet("getbyid")]
        public IActionResult GetById([FromRoute] int id)
        {
            // что-то пошло не так это надо доделать
            _repository.GetById(id);
            return Ok();
        }
    }
}
