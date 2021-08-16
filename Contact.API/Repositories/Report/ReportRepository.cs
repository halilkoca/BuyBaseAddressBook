using Contact.API.Data;
using Contact.API.Entity;
using Contact.API.Model;
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
        public ReportRepository(IContactContext contactContext)
        {
            _contactContext = contactContext ?? throw new ArgumentNullException(nameof(contactContext));
        }

        public async Task<IEnumerable<LocationReportModel>> GenerateLocationReport()
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
                .ToList();

            return response;
        }


    }
}
