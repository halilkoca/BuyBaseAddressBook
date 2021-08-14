using Report.API.Entity;
using Report.API.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Report.API.Repositories
{
    public class ReportRepository : IReportRepository
    {
        public Task<IEnumerable<LocationReportEntity>> Get(RequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}
