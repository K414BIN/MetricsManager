using System;
using System.Collections.Generic;
using System.Text;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace MetricsManagerTests
{   public class RamControllerUnitTests
    {
        private RamMetricsController controller;
        private readonly ILogger<RamMetricsController> _logger;

        public RamControllerUnitTests()
        {
            controller = new RamMetricsController(_logger);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var Memory = 4;

            //Act
            var result = controller.GetMetricsAvailabeMemoryFromAgent(agentId, Memory);
            _logger.Log(LogLevel.Information, "Test result {0} ", result);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
   
}
