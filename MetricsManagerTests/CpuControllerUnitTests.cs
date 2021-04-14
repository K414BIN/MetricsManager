using System;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using MetricsManager;
using Microsoft.Extensions.Logging;

using Moq;

namespace MetricsManagerTests
{
    public class CpuControllerUnitTests
    {
        private CpuMetricsController controller;
    //    private readonly Mock<ICpuMetricsRepository> _mock;
   //     private  Mock<ILogger<CpuMetricsControllerUnitTests>> _mocklogger;
        private readonly ILogger<CpuMetricsController> _logger;

        public CpuControllerUnitTests()
        {
        //    _mock = new Mock<ICpuMetricsRepository>();
          //  _mocklogger = new Mock<ILogger<CpuControllerUnitTests>>();
            controller = new CpuMetricsController(_logger);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act 
            var result = controller.GetMetricsCpuFromAgent(agentId, fromTime, toTime);
            _logger.Log(LogLevel.Information, "Test result {0} ", result);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}



