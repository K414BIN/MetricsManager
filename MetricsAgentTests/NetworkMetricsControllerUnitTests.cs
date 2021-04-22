using System;
using System.Collections.Generic;
using System.Data;
using AutoFixture;
using AutoMapper;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.Models;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Moq;
using Xunit;

namespace MetricsAgentTests
{
    public class NetworkMetricsControllerUnitTests
    {
        private NetworkMetricsController _controller;
        private Mock<ILogger<NetworkMetricsController>> _mockLogger;
        private Mock<INetworkMetricsRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;

        public NetworkMetricsControllerUnitTests()
        {
            _mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            _mockRepository = new Mock<INetworkMetricsRepository>();
            _mockMapper = new Mock<IMapper>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NetworkMetric, NetworkMetricDto>());
            var mapper = config.CreateMapper();
            _controller = new NetworkMetricsController(_mockLogger.Object, _mockRepository.Object, mapper);
        }

        [Fact]
        public void Call_GetNetworkMetricsTimeInterval_From_Controller()
        {
            Random random = new Random();
            var fixture = new Fixture();
            var returnList = fixture.Create<List<NetworkMetric>>();
            _mockRepository
                .Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                    .Returns(returnList);
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(random.Next(50));
            var toTime = DateTimeOffset.FromUnixTimeSeconds(random.Next(50,100));
            var resultGetNetworkMetricsTimeInterval =
                (OkObjectResult) _controller.GetNetworkMetricsTimeInterval(fromTime, toTime);
            var actualResult = (AllMetricsResponse<NetworkMetricDto>) resultGetNetworkMetricsTimeInterval.Value;
            _mockRepository.Verify(repository => repository.GetByTimePeriod(
                    It.Is<DateTimeOffset>(item => item == fromTime),
                    It.Is<DateTimeOffset>(item => item == toTime)),
                Times.Once());
            _ = Assert.IsAssignableFrom<IActionResult > (resultGetNetworkMetricsTimeInterval);
            Assert.Equal(returnList[0].Id,actualResult.Metrics[0].Id);
       
        }

    }
}