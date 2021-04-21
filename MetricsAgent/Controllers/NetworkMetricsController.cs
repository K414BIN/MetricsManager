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
    
    [Route("api/metrics/network")]
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


        [HttpGet("NetworkMetrics/from/{fromTime}/to/{toTime}")]
        public  GetAllNetworkMetricsRequest GetNetworkMetrics([FromRoute] long fromTime, [FromRoute] long toTime)
        {
            _logger.Log(LogLevel.Information, "Requested between time {0} - {1} sec.", fromTime.FromUnixTimeMs(), toTime.FromUnixTimeMs());
            return new GetAllNetworkMetricsRequest
            {
                FromTime = TimeSpan.FromSeconds(fromTime),
                ToTime = TimeSpan.FromSeconds(fromTime)
            };
        }

        //[HttpPost("create")]
        //public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        //{
        //    _repository.Create(new NetworkMetric
        //    {
        //        Time = request.Time,
        //        Value = request.Value
        //    });

        //    return Ok();
        //}
       
       
        //[HttpGet("from/{fromTime}/to/{toTime}")]
        //public IActionResult GetMetricsNetwork([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        //{
        //    _logger.LogInformation($"GetNetworkMetricsTimeInterval - From time: {fromTime}; To time: {toTime}");
        //    //    List<CpuMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
        //    var metrics = _repository.GetAll();
        //    var response = new AllMetricsResponse<NetworkMetricDto>()
        //    {
        //        Metrics = new List<NetworkMetricDto>()

        //    };
        //    foreach (var metric in metrics)
        //    {
        //        response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
        //    }

        //    return Ok(response);
        //}

        //[HttpGet("all")]
        //public IActionResult GetAll()
        //{
        //    _logger.LogInformation($"GetAll");
        //    var metrics = _repository.GetAll();

        //    var response = new AllMetricsResponse<NetworkMetricDto>()
        //    {
        //        Metrics = new List<NetworkMetricDto>()
        //    };

        //    foreach (var metric in metrics)
        //    {
        //        response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
        //    }

        //    return Ok(response);
        //}

        //[HttpDelete("delete/{id}")]
        //public IActionResult Delete([FromRoute] int id)
        //{
        //    _repository.Delete(id);
        //    return Ok();
        //}

        //[HttpPut("update")]
        //public IActionResult Update([FromBody] NetworkMetric request)
        //{
        //    _repository.Update(request);
        //    return Ok();
        //}

        //[HttpGet("getbyid/{id}")]
        //public IActionResult GetById([FromRoute] int id)
        //{
        //    _repository.GetById(id);
        //    return Ok();
        //}
    }
}
