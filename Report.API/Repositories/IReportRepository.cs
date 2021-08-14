using Report.API.Entity;
using Report.API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Report.API.Repositories
{
    public interface IReportRepository
    {
        Task<IEnumerable<LocationReportEntity>> Get(RequestModel model);
    }
}
