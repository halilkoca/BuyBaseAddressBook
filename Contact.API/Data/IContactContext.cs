using MongoDB.Driver;
using Contact.API.Entity;

namespace Contact.API.Data
{
    public interface IContactContext
    {
        IMongoCollection<ContactEntity> Contacts { get; }
    }
}
