using Contact.API.Data;
using Contact.API.Entity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace Contact.API.Repositories.ContactInformation
{
    public class ContactInformationRepository : IContactInformationRepository
    {
        private readonly IContactContext _contactContext;

        public ContactInformationRepository(IContactContext contactContext)
        {
            _contactContext = contactContext ?? throw new ArgumentNullException(nameof(contactContext));
        }

        /// <summary>
        /// A Single New Record into Contact Information
        /// </summary>
        /// <param name="id">Contact UUID</param>
        /// <param name="model">ContactInformation</param>
        /// <returns></returns>
        public async Task<ContactInformationEntity> Create(string id, ContactInformationEntity model)
        {
            try
            {
                var update = Builders<ContactEntity>.Update.Push(x => x.ContactInformations, model);
                var result = await _contactContext.Contacts.UpdateOneAsync(x => x.UUID == id, update);
            }
            catch (MongoWriteException e)
            {
                if (e.WriteConcernError == null)
                {
                    model.UUID = ObjectId.GenerateNewId().ToString();
                    var insert = Builders<ContactEntity>.Update.Set(x => x.ContactInformations, new List<ContactInformationEntity> { model });
                    var res = await _contactContext.Contacts.UpdateOneAsync(x => x.UUID == id, insert);
                }
            }

            return model;
        }

        /// <summary>
        /// Delete One More Than Record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="informationIds">ContactInformation Ids</param>
        /// <returns></returns>
        public async Task<bool> Delete(string id, List<string> informationIds)
        {
            var update = Builders<ContactEntity>.Update.PullFilter(p => p.ContactInformations, f => informationIds.Contains(f.UUID));
            var result = await _contactContext.Contacts.FindOneAndUpdateAsync(p => p.UUID == id, update);
            return true;
        }

        /// <summary>
        /// Delete A Single Record
        /// </summary>
        /// <param name="id">Contact UUID</param>
        /// <param name="informationId">ContactInformation UUID</param>
        /// <returns></returns>
        public async Task<bool> Delete(string id, string informationId)
        {

            var update = Builders<ContactEntity>.Update.PullFilter(p => p.ContactInformations, f => f.UUID == informationId);
            var result = await _contactContext.Contacts.FindOneAndUpdateAsync(p => p.UUID == id, update);
            return true;
        }

        /// <summary>
        /// Update A Single Record in Contact Object 
        /// </summary>
        /// <param name="id">Contact UUID</param>
        /// <param name="model">ContactInformation</param>
        /// <returns></returns>
        public async Task<bool> Update(string id, ContactInformationEntity model)
        {
            var filter = Builders<ContactEntity>.Filter.Eq(x => x.UUID, id)
                & Builders<ContactEntity>.Filter.ElemMatch(x => x.ContactInformations, Builders<ContactInformationEntity>.Filter.Eq(x => x.UUID, model.UUID));
            var update = Builders<ContactEntity>.Update.Set("ContactInformations.$.Value", model.Value);
            var result = await _contactContext.Contacts.UpdateOneAsync(filter, update);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
