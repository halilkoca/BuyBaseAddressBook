using AutoMapper;
using Contact.API.Model;
using EventBus.Messages.Events;

namespace Contact.API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<LocationReportModel, LocationReportEvent>().ReverseMap();
        }
    }
}
