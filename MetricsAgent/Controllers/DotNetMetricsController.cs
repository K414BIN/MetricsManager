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
        
        //[HttpGet("errors-count/from/{fromTime}/to/{toTime}/errorsCount/{errorsCount}")]
        //public IActionResult GetMetricsErrorsCount([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] int errorsCount)
        //{
        //    return Ok();
        //}

        //[HttpDelete("delete /{id}")]
        //public IActionResult Delete([FromRoute] int id)
        //{
        //    _repository.Delete(id);
        //    return Ok();
        //}

        //[HttpPut("update")]
        //public IActionResult Update([FromBody] DotNetMetric request)
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

        //[HttpPost("create")]
        //public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        //{
        //    _repository.Create(new DotNetMetric
        //    {
        //        Time = request.Time,
        //        Value = request.Value
        //    });

        //    return Ok();
        //}

        //[HttpGet("all")]
        //public IActionResult GetAll()
        //{ 
        //    _logger.LogInformation($"GetAll");
        //    var metrics = _repository.GetAll();

        //    var response = new AllMetricsResponse<DotNetMetricDto>()
        //    {
        //        Metrics = new List<DotNetMetricDto>()
        //    };

        //    foreach (var metric in metrics)
        //    {
        //        response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
        //    }

        //    return Ok(response);
        //}

        [HttpGet("DotNetMetrics/from/{fromTime}/to/{toTime}")]
        public  GetAllDotNetMetricsRequest GetDotNetMetrics([FromRoute] long fromTime, [FromRoute] long toTime)
        { 
            _logger.Log(LogLevel.Information, "Requested between time {0} - {1} sec.", fromTime.FromUnixTimeMs(), toTime.FromUnixTimeMs());
            return new GetAllDotNetMetricsRequest
            {
                FromTime = TimeSpan.FromSeconds(fromTime),
                ToTime = TimeSpan.FromSeconds(fromTime)
            };
        }
    }
}
