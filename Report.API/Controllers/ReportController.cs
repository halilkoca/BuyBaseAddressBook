using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Report.API.Entity;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Report.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;

        public ReportController(ILogger<ReportController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LocationReportEntity>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<LocationReportEntity>>> Get()
        {
            return Ok();
        }


    }
}
