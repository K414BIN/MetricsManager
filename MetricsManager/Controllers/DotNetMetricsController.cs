using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        [HttpGet("agent/{agentId}/errors-count/from/{fromTime}/to/{toTime}/errorstype/{errorstype}")]
        public IActionResult GetMetricsErrorsCountFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] ErrorsType errorsType)
        {
            return Ok();
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}/errorstype/{errorstype}")]
        public IActionResult GetMetricsErrorsCount([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] ErrorsType errorsType)
        {
            return Ok();
        }
    }
}
