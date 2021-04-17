using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.Models;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // добавлять сопоставления в таком стиле нужно для всех объектов 
            CreateMap<HddMetric, HddMetricDto>();
            CreateMap<RamMetric, RamMetricDto>();
            CreateMap<NetworkMetric, NetworkMetricDto>();
            CreateMap<DotNetMetric, DotNetMetricDto>();
            CreateMap<CpuMetric, CpuMetricDto>();

        }

    }
}
