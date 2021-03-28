using System;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuControllerUnitTests
    {
        private CpuMetricsController controller;

        public CpuControllerUnitTests()
        {
            controller = new CpuMetricsController();
        }

        [Fact]
        public void GetMetricsFrom_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetMetricsFrom(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsByPercentileFrom_ReturnsOk()
        {

            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var capacity = Percentile.P95;

            //Act
            var result = controller.GetMetricsByPercentileFrom(fromTime, toTime, capacity);

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
        public void GetMetricsFrom_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetMetricsNetwork(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
    public class HddControllerUnitTests
    {
        private HddMetricsController controller;

        public HddControllerUnitTests()
        {
            controller = new HddMetricsController();
        }

        [Fact]
        public void GetMetricsFrom_ReturnsOk()
        { 
            //Arrange
            var whichMemory = MemoryInGB.FreeLeft;

            //Act
            var result = controller.GetMetricsFreeLeftMemory(whichMemory);

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
        public void GetMetricsFrom_ReturnsOk()
        {
            //Arrange
            var whichMemory = MemoryInGB.FreeLeft;

            //Act
            var result = controller.GetMetricsAvailabeMemory(whichMemory);

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
        public void GetMetricsFrom_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var kindOfErrors = ErrorsType.All;

            //Act
            var result = controller.GetMetricsErrorsCount( fromTime, toTime, kindOfErrors);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}