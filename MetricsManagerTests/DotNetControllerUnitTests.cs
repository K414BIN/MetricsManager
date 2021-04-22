using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MetricsManagerTests
{
    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController controller;
        private readonly ILogger<DotNetMetricsController> _logger;

        public DotNetControllerUnitTests()
        {
            controller = new DotNetMetricsController(_logger);
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