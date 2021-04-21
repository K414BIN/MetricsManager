using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator.Builders.Update;

namespace MetricsManager.Responses
{
    public class ApiResponse<T> where T :class
    {
        public List<ApiMetric<T>> Metrics;
    }
}
