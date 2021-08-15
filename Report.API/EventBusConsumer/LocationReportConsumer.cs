using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Report.API.Entity;
using Report.API.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Report.API.EventBusConsumer
{
    public class LocationReportConsumer : IConsumer<LocationReportEventList>
    {

        private readonly IDistributedCache _redisCache;
        private readonly IMapper _mapper;

        public LocationReportConsumer(IDistributedCache redisCache, IMapper mapper)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<LocationReportEventList> context)
        {
            var report = _mapper.Map<List<LocationReportEntity>>(context.Message.LocationReportEvents);
            await _redisCache.SetStringAsync($"{ReportKey.LocationReport}", JsonConvert.SerializeObject(report));
        }
    }
}
