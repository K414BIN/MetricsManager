using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator.Builders.Update;
using MetricsManager.Models;

namespace MetricsManager.Responses
{
    public class ApiResponse<T> where T :class
    {
        public List<T> Metrics;
    }
}
