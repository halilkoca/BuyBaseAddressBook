using MongoDB.Driver;
using Contact.API.Entity;
using Microsoft.Extensions.Configuration;
using System;

namespace Contact.API.Data
{
    public class ContactContext : IContactContext, IDisposable
    {
        public ContactContext()
        {
            var client = new MongoClient(Startup.Configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(Startup.Configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Contacts = database.GetCollection<ContactEntity>(Startup.Configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }

        public IMongoCollection<ContactEntity> Contacts { get; }

        public void Dispose()
        {
        }
    }
}
