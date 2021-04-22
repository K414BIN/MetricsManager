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
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://localhost:51684/api/NetworkMetrics/from/1/to/999999");

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
                    <AllNetworkMetricsApiResponse>(responseStream).Result;
            }
            else
            {
                // ошибка при получении ответа
            }
            return Ok();
        }
    }
    
}