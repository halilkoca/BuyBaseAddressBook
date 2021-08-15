using AutoMapper;
using Contact.API.Model;
using EventBus.Messages.Events;

namespace Contact.API.Mapper
{
    public class LocationReport : Profile
    {
        public LocationReport()
        {
            CreateMap<LocationReportModel, LocationReportEvent>().ReverseMap();
        }
    }
}
