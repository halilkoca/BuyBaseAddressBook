using Microsoft.Extensions.Caching.Distributed;
using Report.API.Entity;
using Report.API.Model;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Report.API.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly IDistributedCache _redisCache;


        public ReportRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<IEnumerable<LocationReportEntity>> Get(RequestModel model)
        {
            throw new NotImplementedException("Implement Data From other service");


            var cache = await _redisCache.GetStringAsync($"{ReportKey.LocationReport}_{model.PageNumber}_{model.PageSize}");
            if (string.IsNullOrEmpty(cache))
            {
                // get data 
                // contact.api -> 
                await _redisCache.SetStringAsync($"{ReportKey.LocationReport}_{model.PageNumber}_{model.PageSize}", "");
            }

            var value = JsonSerializer.Deserialize<IEnumerable<LocationReportEntity>>(cache);
            return value;
        }
    }
}
