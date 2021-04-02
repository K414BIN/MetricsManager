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

namespace MetricsAgentTests
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController controller;
        private Mock<ICpuMetricsRepository> mock;

        public CpuMetricsControllerUnitTests()
        {
            mock = new Mock<ICpuMetricsRepository>();

            controller = new CpuMetricsController(mock.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = controller.Create(new CpuMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
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

        public RamControllerUnitTests()
        {
            controller = new RamMetricsController();
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