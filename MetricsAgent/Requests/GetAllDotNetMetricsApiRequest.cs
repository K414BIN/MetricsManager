﻿using System;

namespace MetricsAgent.Requests
{
   public class GetAllDotNetMetricsApiRequest
    {
        public Uri AgentAddress { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}