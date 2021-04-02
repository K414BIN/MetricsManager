using System;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using MetricsAgent;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Requests;
using MainLibrary;
using MetricsAgent.DAL.Models;
using Microsoft.Extensions.Logging;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController controller;
        private readonly Mock<ICpuMetricsRepository> _mock;
        private  Mock<ILogger<CpuMetricsControllerUnitTests>> _mocklogger;

        public CpuMetricsControllerUnitTests()
        {
            _mock = new Mock<ICpuMetricsRepository>();
            _mocklogger = new Mock<ILogger<CpuMetricsControllerUnitTests>>();
            controller = new CpuMetricsController(_mock.Object);
        }

        [Fact]
        public void GetMetricsFrom_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetMetricsCpu(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            _mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = controller.Create(new CpuMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }
    }

    public class NetworkControllerUnitTests
        {
        private NetworkMetricsController controller;
        private readonly Mock<INetworkMetricsRepository> _mock;
        private Mock<ILogger<NetworkControllerUnitTests>> _mocklogger;
        
        public NetworkControllerUnitTests()
        {
            _mock = new Mock<INetworkMetricsRepository>();
            controller = new NetworkMetricsController(_mock.Object);
            _mocklogger = new Mock<ILogger<NetworkControllerUnitTests>>();
            
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
          
            _mock.Setup(repository => repository.Create(It.IsAny< NetworkMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = controller.Create(new NetworkMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            _mock.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()), Times.AtMostOnce());
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
        private readonly Mock<IHddMetricsRepository> _mock;
        private Mock<ILogger<HddControllerUnitTests>> _mocklogger;

     
        public HddControllerUnitTests()
        {
            _mocklogger = new Mock<ILogger<HddControllerUnitTests>>();
            controller = new HddMetricsController(_mock.Object);
            _mock = new Mock<IHddMetricsRepository>();
        }

        [Fact]
        public void GetMetricsFrom_ReturnsOk()
        { 
            //Arrange
            var whichMemory = MemoryInGb.FreeLeft;

            //Act
            var result = controller.GetMetricsFreeLeftMemory(whichMemory);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
    public class RamControllerUnitTests
    {
        private RamMetricsController controller;
        private Mock<ILogger<RamControllerUnitTests>> _mocklogger;
        private readonly Mock<IRamMetricsRepository> _mock;

        public RamControllerUnitTests()
        {
            _mock = new Mock<IRamMetricsRepository>();
            _mocklogger = new Mock<ILogger<RamControllerUnitTests>>();
            controller = new RamMetricsController(_mock.Object);
        }

        [Fact]
        public void GetMetricsFrom_ReturnsOk()
        {
            //Arrange
            var whichMemory = MemoryInGb.FreeLeft;

            //Act
            var result = controller.GetMetricsAvailabeMemory(whichMemory);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
    public class DotNetControllerUnitTests
    {
        private DotNetMetricsController controller;
        private Mock<IDotNetMetricsRepository> _mock;
        private Mock<ILogger<DotNetControllerUnitTests>> _mocklogger;

        public DotNetControllerUnitTests()
        {
            controller = new DotNetMetricsController(_mock.Object);
            _mock = new Mock<IDotNetMetricsRepository>();
            _mocklogger = new Mock<ILogger<DotNetControllerUnitTests>>();
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