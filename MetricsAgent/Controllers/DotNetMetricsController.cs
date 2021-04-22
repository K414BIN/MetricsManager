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
    public class DotNetMetricsController : ControllerBase
    {
        private readonly IDotNetMetricsRepository _repository;
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IMapper _mapper;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _logger.LogInformation("Start DotNetMetricsController");
        }
        

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public  GetAllDotNetMetricsRequest GetDotNetMetrics([FromRoute] long fromTime, [FromRoute] long toTime)
        { 
            _logger.Log(LogLevel.Information, "Requested between time {0} - {1} sec.", fromTime.FromUnixTimeMs(), toTime.FromUnixTimeMs());
            return new GetAllDotNetMetricsRequest
            {
                FromTime = TimeSpan.FromSeconds(fromTime),
                ToTime = TimeSpan.FromSeconds(fromTime)
            };
        }

        [HttpGet("/from/{fromTime}/to/{toTime}")]
        public IActionResult GetDotNetMetricsTimeInterval([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"GetDotNetMetricsTimeInterval - From time: {fromTime}; To time: {toTime}");
              List<DotNetMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            //     var metrics = _repository.GetAll();
            var response = new AllMetricsResponse<DotNetMetricDto>()
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
            }
            return Ok(response);
        }
    }
}
