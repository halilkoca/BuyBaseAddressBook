using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Report.API.Entity;
using Report.API.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Report.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IDistributedCache _redisCache;

        public ReportController(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LocationReportEntity>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Location()
        {
            var model = await _redisCache.GetStringAsync(ReportKey.LocationReport);
            return Ok(JsonConvert.DeserializeObject<List<LocationReportEntity>>(model));
        }


    }
}
