using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using ServiceStack.Text;

namespace MetricsAgent.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly IHddMetricsRepository _repository;
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IMapper _mapper;

        public HddMetricsController(ILogger<HddMetricsController> logger, IHddMetricsRepository repository,
           IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _logger.LogInformation("Start HddMetricsController");
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public  GetAllHddMetricsRequest GetHddMetrics([FromRoute] long fromTime, [FromRoute] long toTime)
        {
            _logger.Log(LogLevel.Information, "Requested between time {0} - {1} sec.", fromTime.FromUnixTimeMs(), toTime.FromUnixTimeMs());
            return new GetAllHddMetricsRequest
            {
                FromTime = TimeSpan.FromSeconds(fromTime),
                ToTime = TimeSpan.FromSeconds(fromTime)
            };
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetHddMetricsTimeInterval([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"GetHddMetricsTimeInterval - From time: {fromTime}; To time: {toTime}");
                List<HddMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            //var metrics = _repository.GetAll();
            var response = new AllMetricsResponse<HddMetricDto>()
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }
            return Ok(response);
        }
    }
}
