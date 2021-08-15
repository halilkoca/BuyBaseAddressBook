using Contact.API.Entity;
using Contact.API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contact.API.Repositories.ContactInformation
{
    public interface IContactInformationRepository
    {


        Task<ContactInformationEntity> Create(string id, ContactInformationEntity model);
        Task<bool> Delete(string id, List<string> informationIds);
        Task<bool> Delete(string id, string informationId);
        Task<bool> Update(string id, ContactInformationEntity model);
    }
}
