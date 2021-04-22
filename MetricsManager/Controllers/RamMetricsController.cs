using System;
using Core;
using MetricsManager.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using MetricsManager.Responses;
using NLog;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
            private readonly ILogger<RamMetricsController> _logger;

            public RamMetricsController(ILogger<RamMetricsController> logger)
            {
                _logger = logger;
                _logger.LogDebug(1, "NLog встроен в RamMetricsController");
            }

            [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
            public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    "http://localhost:51684/api/RamMetrics/from/1/to/999999");

                _logger.LogInformation(2,"Request {0} ", request);

                request.Headers.Add("Accept", "application/vnd.github.v3+json");
                //var client = clientFactory.CreateClient();
                var client =new HttpClient();
                //
                HttpResponseMessage response = client.SendAsync(request).Result;

                _logger.LogInformation(3,"Response {0} ", response);

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = response.Content.ReadAsStreamAsync().Result;
                    var metricsResponse = JsonSerializer.DeserializeAsync
                        <AllRamMetricsApiResponse>(responseStream).Result;
                }
                else
                {
                    // ошибка при получении ответа
                }
                return Ok();
            }
        }
    
}
