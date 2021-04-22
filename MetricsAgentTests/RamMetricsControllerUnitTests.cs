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
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController _controller;
        private Mock<ILogger<RamMetricsController>> _mockLogger;
        private Mock<IRamMetricsRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;

        public RamMetricsControllerUnitTests()
        {
            _mockLogger = new Mock<ILogger<RamMetricsController>>();
            _mockRepository = new Mock<IRamMetricsRepository>();
            _mockMapper = new Mock<IMapper>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RamMetric, RamMetricDto>());
            var mapper = config.CreateMapper();
            _controller = new RamMetricsController(_mockLogger.Object, _mockRepository.Object, mapper);
        }

        [Fact]
        public void Call_GetRamMetricsTimeInterval_From_Controller()
        {
            Random random = new Random();
            var fixture = new Fixture();
            var returnList = fixture.Create<List<RamMetric>>();
            _mockRepository
                .Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                    .Returns(returnList);
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(random.Next(50));
            var toTime = DateTimeOffset.FromUnixTimeSeconds(random.Next(50,100));
            var resultGetRamMetricsTimeInterval =
                (OkObjectResult) _controller.GetRamMetricsTimeInterval(fromTime, toTime);
            var actualResult = (AllMetricsResponse<RamMetricDto>) resultGetRamMetricsTimeInterval.Value;
            _mockRepository.Verify(repository => repository.GetByTimePeriod(
                    It.Is<DateTimeOffset>(item => item == fromTime),
                    It.Is<DateTimeOffset>(item => item == toTime)),
                Times.Once());
            _ = Assert.IsAssignableFrom<IActionResult > (resultGetRamMetricsTimeInterval);
            Assert.Equal(returnList[0].Id,actualResult.Metrics[0].Id);
        
        }

    }
}