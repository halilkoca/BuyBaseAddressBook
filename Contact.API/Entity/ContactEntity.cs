using System.Collections.Generic;

namespace Contact.API.Entity
{
    public class ContactEntity : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Firm { get; set; }


        //Model One-to-Many Relationships with Embedded Documents
        public virtual ICollection<ContactInformationEntity> ContactInformations { get; set; }

    }
}
