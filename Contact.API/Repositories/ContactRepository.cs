using Contact.API.Data;
using Contact.API.Entity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contact.API.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IContactContext _contactContext;
        public ContactRepository(IContactContext contactContext)
        {
            _contactContext = contactContext ?? throw new ArgumentNullException(nameof(contactContext));
        }

        public async Task<List<ContactEntity>> Create(List<ContactEntity> models)
        {
            await _contactContext.Contacts.InsertManyAsync(models);
            return models;
        }

        public async Task<ContactEntity> Create(ContactEntity model)
        {
            await _contactContext.Contacts.InsertOneAsync(model);
            return model;
        }

        public async Task<bool> Delete(List<string> ids)
        {
            DeleteResult result = await _contactContext.Contacts.DeleteManyAsync(x => ids.Contains(x.UUID));
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            DeleteResult result = await _contactContext.Contacts.DeleteOneAsync(x => x.UUID == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
        public async Task<IEnumerable<ContactEntity>> Get()
        {
            return await _contactContext.Contacts.Find(p => true).ToListAsync();
        }

        public async Task<ContactEntity> Get(string id)
        {
            return await _contactContext.Contacts.Find(c => c.UUID == id).FirstOrDefaultAsync();
        }

        public async Task<ContactEntity> GetByName(string name)
        {
            FilterDefinition<ContactEntity> filter = Builders<ContactEntity>.Filter.ElemMatch(p => p.UUID, name);
            return await _contactContext.Contacts.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<ContactEntity> Update(ContactEntity model)
        {
            await _contactContext.Contacts.ReplaceOneAsync(c => c.UUID == model.UUID, model);
            return model;
        }
    }
}
