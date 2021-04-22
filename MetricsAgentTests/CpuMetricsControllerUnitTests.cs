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
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController _controller;
        private Mock<ILogger<CpuMetricsController>> _mockLogger;
        private Mock<ICpuMetricsRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;

        public CpuMetricsControllerUnitTests()
        {
            _mockLogger = new Mock<ILogger<CpuMetricsController>>();
            _mockRepository = new Mock<ICpuMetricsRepository>();
            _mockMapper = new Mock<IMapper>();
            //var config = new MapperConfiguration(
            //    cfg => cfg.CreateMap<CpuMetric, CpuMetricDto>()
            //        .ForMember(dto => dto.Time,
            //            o => o.MapFrom(dbModel => TimeSpan.FromSeconds(dbModel.Time.TotalSeconds()))));
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CpuMetric, CpuMetricDto>());
            var mapper = config.CreateMapper();
            _controller = new CpuMetricsController(_mockLogger.Object, _mockRepository.Object, mapper);
        }

        [Fact]
        public void Call_GetCpuMetricsTimeInterval_From_Controller()
        {
            Random random = new Random();
            var fixture = new Fixture();
            var returnList = fixture.Create<List<CpuMetric>>();
            _mockRepository
                .Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()))
                    .Returns(returnList);
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(random.Next(50));
            var toTime = DateTimeOffset.FromUnixTimeSeconds(random.Next(50,100));
            var resultGetCpuMetricsTimeInterval =
                (OkObjectResult) _controller.GetCpuMetricsTimeInterval(fromTime, toTime);
            var actualResult = (AllMetricsResponse<CpuMetricDto>) resultGetCpuMetricsTimeInterval.Value;
            _mockRepository.Verify(repository => repository.GetByTimePeriod(
                    It.Is<DateTimeOffset>(item => item == fromTime),
                    It.Is<DateTimeOffset>(item => item == toTime)),
                Times.Once());
            _ = Assert.IsAssignableFrom<IActionResult > (resultGetCpuMetricsTimeInterval);
            Assert.Equal(returnList[0].Id,actualResult.Metrics[0].Id);
          //      Assert.Equal(  resultGetCpuMetricsTimeInterval.Value);
        }

    }
}