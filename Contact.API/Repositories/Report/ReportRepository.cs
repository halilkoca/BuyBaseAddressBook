using AutoMapper;
using Contact.API.Data;
using Contact.API.Entity;
using Contact.API.Model;
using EventBus.Messages.Events;
using MassTransit;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Repositories.Report
{
    public class ReportRepository : IReportRepository
    {
        private readonly IContactContext _contactContext;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public ReportRepository(IContactContext contactContext, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _contactContext = contactContext ?? throw new ArgumentNullException(nameof(contactContext));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private async Task<IEnumerable<LocationReportModel>> GenerateLocationReport()
        {
            var result = await _contactContext.Contacts.AsQueryable().Where(x =>
                x.ContactInformations.Any(a => a.Type == InformationType.Location)
                && x.ContactInformations.Any(a => a.Type == InformationType.PhoneNumber))
                .ToListAsync();

            var response = result
                .Select(x => new
                {
                    Location = x.ContactInformations.Where(b => b.Type == InformationType.Location).FirstOrDefault().Value,
                    LocationCount = x.ContactInformations.Where(a => a.Type == InformationType.Location).Count(),
                    PeopleCount = 1,
                    PhoneNumberCount = x.ContactInformations.Where(a => a.Type == InformationType.PhoneNumber).Count()
                })
                .GroupBy(b => b.Location)
                .Select(a => new LocationReportModel
                {
                    Location = a.Key,
                    LocationCount = a.Sum(b => b.LocationCount),
                    PeopleCount = a.Sum(b => b.PeopleCount),
                    PhoneNumberCount = a.Sum(b => b.PhoneNumberCount),
                })
                .OrderByDescending(a => a.LocationCount)
                .ToList();

            return response;
        }

        public async Task GenerateReport()
        {
            // publish event 
            var result = await GenerateLocationReport();
            var locationReport = _mapper.Map<List<LocationReportEvent>>(result);
            await _publishEndpoint.Publish(new LocationReportEventList { LocationReportEvents = locationReport });
        }


    }
}
