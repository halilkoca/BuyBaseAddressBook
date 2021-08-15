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
            var result = await _contactContext.Contacts.AsQueryable<ContactEntity>()
                .Where(x => x.ContactInformations.Any(a => a.Type == InformationType.Location))
                .ToListAsync();

            return new List<LocationReportModel>();
        }


    }
}
