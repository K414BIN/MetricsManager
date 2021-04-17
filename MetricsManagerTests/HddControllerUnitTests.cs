using System;
using System.Collections.Generic;
using System.Text;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace MetricsManagerTests
{
    public class HddControllerUnitTests
        {
            private HddMetricsController controller;
            private readonly ILogger<HddMetricsController> _logger;
        
            public HddControllerUnitTests()
            {
             controller = new HddMetricsController(_logger);
            }

            [Fact]
            public void GetMetricsFromAgent_ReturnsOk()
            {
                //Arrange
                var agentId = 1;
                var Memory = 4;

                //Act
                var result = controller.GetMetricsFreeLeftMemoryFromAgent(agentId, Memory);
                _logger.Log(LogLevel.Information, "Test result {0} ", result);
                // Assert
                _ = Assert.IsAssignableFrom<IActionResult>(result);
            }
    }
}
