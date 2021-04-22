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
    public class NetworkMetricsController : ControllerBase
    {
         private readonly INetworkMetricsRepository _repository;
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IMapper _mapper;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricsRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _logger.LogInformation("Start NetworkMetricsController");
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public  GetAllNetworkMetricsRequest GetNetworkMetrics([FromRoute] long fromTime, [FromRoute] long toTime)
        {
            _logger.Log(LogLevel.Information, "Requested between time {0} - {1} sec.", fromTime.FromUnixTimeMs(), toTime.FromUnixTimeMs());
            return new GetAllNetworkMetricsRequest
            {
                FromTime = TimeSpan.FromSeconds(fromTime),
                ToTime = TimeSpan.FromSeconds(fromTime)
            };
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetworkMetricsTimeInterval([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"GetNetworkMetricsTimeInterval - From time: {fromTime}; To time: {toTime}");
            List<NetworkMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            //var metrics = _repository.GetAll();
            var response = new AllMetricsResponse<NetworkMetricDto>()
            {
                Metrics = new List<NetworkMetricDto>()

            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }

            return Ok(response);
        }

    }
}
