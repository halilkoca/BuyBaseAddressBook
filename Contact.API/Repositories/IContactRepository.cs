﻿using Contact.API.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contact.API.Repositories
{
    public interface IContactRepository
    {
        Task<List<ContactEntity>> Create(List<ContactEntity> models);
        Task<ContactEntity> Create(ContactEntity model);
        Task<bool> Delete(List<string> ids);
        Task<bool> Delete(string id);
        Task<ContactEntity> Get(string id);
        Task<ContactEntity> GetByName(string name);
        Task<ContactEntity> Update(ContactEntity model);
    }
}
