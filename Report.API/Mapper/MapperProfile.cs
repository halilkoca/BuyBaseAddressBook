using AutoMapper;
using EventBus.Messages.Events;
using Report.API.Entity;
using System.Collections.Generic;

namespace Report.API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<LocationReportEntity, LocationReportEvent>().ReverseMap();
        }
    }
}
