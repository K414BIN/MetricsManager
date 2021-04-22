using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceStack.Text;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly IRamMetricsRepository _repository;
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IMapper _mapper;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository repository,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _logger.LogInformation("Start RamMetricsController");
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public GetAllRamMetricsRequest GetRamMetrics([FromRoute] long fromTime, [FromRoute] long toTime)
        {
            _logger.Log(LogLevel.Information, "Requested between time {0} - {1} sec.", fromTime.FromUnixTimeMs(), toTime.FromUnixTimeMs());
            return new GetAllRamMetricsRequest
            {
                FromTime = TimeSpan.FromSeconds(fromTime),
                ToTime = TimeSpan.FromSeconds(fromTime)
            };
        }

        //[HttpGet("from/{fromTime}/to/{toTime}")]
        //public IActionResult GetRamMetricsTimeInterval ([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        //{
        //    _logger.LogInformation($"GetRamMetricsTimeInterval - From time: {fromTime}; To time: {toTime}");
      
        //    var metrics = _repository.GetAll();
        //    var response = new AllMetricsResponse<RamMetricDto>()
        //    {
        //        Metrics = new List<RamMetricDto>()

        //    };
        //    foreach (var metric in metrics)
        //    {
        //        response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
        //    }

        //    return Ok(response);
        //}

    }
}