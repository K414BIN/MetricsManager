using System;
using System.Collections.Generic;
using System.Text;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace MetricsManagerTests
{
  public  class NetworkControllerUnitTests
    {
        private NetworkMetricsController controller;
        
        private readonly ILogger<NetworkMetricsController> _logger;
        public NetworkControllerUnitTests()
        {
            controller = new NetworkMetricsController(_logger);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
 
            //Act
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
           _logger.Log(LogLevel.Information, "Test result {0} ", result);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
