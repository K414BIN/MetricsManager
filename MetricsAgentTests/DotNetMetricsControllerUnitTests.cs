﻿using System;
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
    public class DotNetMetricsControllerUnitTests
    {
        private DotNetMetricsController _controller;
        private Mock<ILogger<DotNetMetricsController>> _mockLogger;
        private Mock<IDotNetMetricsRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;

        public DotNetMetricsControllerUnitTests()
        {
            _mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            _mockRepository = new Mock<IDotNetMetricsRepository>();
            _mockMapper = new Mock<IMapper>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DotNetMetric, DotNetMetricDto>());
            var mapper = config.CreateMapper();
            _controller = new DotNetMetricsController(_mockLogger.Object, _mockRepository.Object, mapper);
        }

        [Fact]
        public void Call_GetDotNetMetricsTimeInterval_From_Controller()
        {
            Random random = new Random();
            var fixture = new Fixture();
            var returnList = fixture.Create<List<DotNetMetric>>();
            _mockRepository
                .Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                    .Returns(returnList);
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(random.Next(50));
            var toTime = DateTimeOffset.FromUnixTimeSeconds(random.Next(50,100));
            var resultGetDotNetMetricsTimeInterval =
                (OkObjectResult) _controller.GetDotNetMetricsTimeInterval(fromTime, toTime);
            var actualResult = (AllMetricsResponse<DotNetMetricDto>) resultGetDotNetMetricsTimeInterval.Value;
            _mockRepository.Verify(repository => repository.GetByTimePeriod(
                    It.Is<DateTimeOffset>(item => item == fromTime),
                    It.Is<DateTimeOffset>(item => item == toTime)),
                Times.Once());
            _ = Assert.IsAssignableFrom<IActionResult > (resultGetDotNetMetricsTimeInterval);
            Assert.Equal(returnList[0].Id,actualResult.Metrics[0].Id);
          
        }

    }
}