using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Collections.Generic;

namespace Contact.API.Entity
{
    public class ContactEntity
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UUID { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Firm { get; set; }


        //Model One-to-Many Relationships with Embedded Documents
        public virtual ICollection<ContactInformationEntity> ContactInformations { get; set; }

    }
}
