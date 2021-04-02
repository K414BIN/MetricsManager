using System;
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
    public class DotNetMetricsController : ControllerBase
    {
        [HttpGet("errors-count/from/{fromTime}/to/{toTime}/errorstype/{errorstype}")]
        public IActionResult GetMetricsErrorsCount([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] ErrorsType errorsType)
        {
            return Ok();
        }
    }
}
