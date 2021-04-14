using System;
using System.Data;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CpuMetric, CpuMetricDto>());
            var mapper = config.CreateMapper();
            _controller = new CpuMetricsController(_mockLogger.Object, _mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public void Call_GetCpuMetricsTimeInterval_From_Controller()
        {
            _mockRepository
                .Setup(repository => repository.GetByTimePeriod(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())
                .Returns(new List<CpuMetric>());

        }
    }
}