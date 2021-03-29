using System;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsManagerTests
{
   
    public class HddControllerUnitTests
    {
        private HddMetricsController controller;

        public HddControllerUnitTests()
        {
            controller = new HddMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;

            //Act
            var result = controller.GetMetricsFreeLeftMemoryFromAgent(agentId, MemoryInGB.FreeLeft);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

    public class RamControllerUnitTests
    {
        private RamMetricsController controller;

        public RamControllerUnitTests()
        {
            controller = new RamMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var whichMemory= MemoryInGB.FreeLeft;

            //Act
            var result = controller.GetMetricsAvailabeMemoryFromAgent(agentId, whichMemory);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController controller;

        public DotNetControllerUnitTests()
        {
            controller = new DotNetMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var kindOfErrors = ErrorsType.All;

            //Act
            var result = controller.GetMetricsErrorsCountFromAgent(agentId, fromTime, toTime, kindOfErrors);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

    public class CpuControllerUnitTests
    {
        private CpuMetricsController controller;

        public CpuControllerUnitTests()
        {
            controller = new CpuMetricsController();
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

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsByPercentileFromAgent_ReturnsOk()
        {

            //Arrange
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var capacity = Percentile.P95;

            //Act
            var result = controller.GetMetricsByPercentileFromAgent(agentId, fromTime, toTime, capacity);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

    public class NetworkControllerUnitTests
        {
        private NetworkMetricsController controller;

        public NetworkControllerUnitTests()
        {
            controller = new NetworkMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
 
            //Act
            var result = controller.GetMetricsNetworkFromAgent(agentId, fromTime, toTime);
            
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}



