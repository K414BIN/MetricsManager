﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        [HttpGet("available/memoryinGb/{memoryingb}")]
        public IActionResult GetMetricsAvailabeMemory([FromRoute] MemoryInGb memoryInGB)
        {
            return Ok();
        }
    }
}
