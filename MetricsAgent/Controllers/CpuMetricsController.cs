using System;
using AutoMapper;
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
using ServiceStack;
using ServiceStack.Text;

namespace MetricsAgent.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMapper _mapper;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, ICpuMetricsRepository repository, IMapper mapper)
        {
         //   var config = new MapperConfiguration(cfg => cfg.CreateMap<CpuMetric, CpuMetricDto>());
            //var mapper = config.CreateMapper();


            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _logger.LogInformation("Start CpuMetricsController");
        }


        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{Percentile}")]
        public IActionResult GetCpuMetricsByPercentileTimeInterval([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"GetCpuMetricsByPercentileTimeInterval - From time : {fromTime};  To time: { toTime};  Percentile {percentile}");

            return Ok();
        }

        //[HttpGet("/from/{fromTime}/to/{toTime}")]
        //public  GetAllCpuMetricsRequest GetCpuMetrics([FromRoute] long fromTime, [FromRoute] long toTime)
        //{
        //    _logger.Log(LogLevel.Information, "Requested between time {0} - {1} sec.", fromTime.FromUnixTimeMs(), toTime.FromUnixTimeMs());
        //    return new GetAllCpuMetricsRequest
        //    {
        //        FromTime = TimeSpan.FromSeconds(fromTime),
        //        ToTime = TimeSpan.FromSeconds(fromTime)
        //    };
        //}

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetCpuMetricsTimeInterval([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"GetCpuMetricsTimeInterval - From time: {fromTime}; To time: {toTime}");
              List<CpuMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
           // var metrics = _repository.GetAll();
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
    }
}
