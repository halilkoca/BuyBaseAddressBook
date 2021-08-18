using Contact.API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contact.API.Repositories.Report
{
    public interface IReportRepository
    {
        Task GenerateReport();
    }
}
