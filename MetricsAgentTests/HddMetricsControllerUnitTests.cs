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
    public class HddMetricsControllerUnitTests
    {
        private HddMetricsController _controller;
        private Mock<ILogger<HddMetricsController>> _mockLogger;
        private Mock<IHddMetricsRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;

        public HddMetricsControllerUnitTests()
        {
            _mockLogger = new Mock<ILogger<HddMetricsController>>();
            _mockRepository = new Mock<IHddMetricsRepository>();
            _mockMapper = new Mock<IMapper>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<HddMetric, HddMetricDto>());
            var mapper = config.CreateMapper();
            _controller = new HddMetricsController(_mockLogger.Object, _mockRepository.Object, mapper);
        }

        [Fact]
        public void Call_GetHddMetricsTimeInterval_From_Controller()
        {
            Random random = new Random();
            var fixture = new Fixture();
            var returnList = fixture.Create<List<HddMetric>>();
            _mockRepository
                .Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                    .Returns(returnList);
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(random.Next(50));
            var toTime = DateTimeOffset.FromUnixTimeSeconds(random.Next(50,100));
            var resultGetHddMetricsTimeInterval =
                (OkObjectResult) _controller.GetHddMetricsTimeInterval(fromTime, toTime);
            var actualResult = (AllMetricsResponse<HddMetricDto>) resultGetHddMetricsTimeInterval.Value;
            _mockRepository.Verify(repository => repository.GetByTimePeriod(
                    It.Is<DateTimeOffset>(item => item == fromTime),
                    It.Is<DateTimeOffset>(item => item == toTime)),
                Times.Once());
            _ = Assert.IsAssignableFrom<IActionResult > (resultGetHddMetricsTimeInterval);
            Assert.Equal(returnList[0].Id,actualResult.Metrics[0].Id);

        }

    }
}