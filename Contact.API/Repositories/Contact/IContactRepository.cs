using Contact.API.Entity;
using Contact.API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contact.API.Repositories.Contact
{
    public interface IContactRepository
    {
        Task<List<ContactEntity>> Create(List<ContactEntity> models);
        Task<ContactEntity> Create(ContactEntity model);
        Task<bool> Delete(List<string> ids);
        Task<bool> Delete(string id);
        Task<IEnumerable<ContactEntity>> Get(RequestModel model);
        Task<ContactEntity> Get(string id);
        Task<ContactEntity> GetByName(string name);
        Task<ContactEntity> Update(ContactEntity model);
    }
}
