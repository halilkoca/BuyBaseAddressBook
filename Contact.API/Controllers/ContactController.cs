using Contact.API.Entity;
using Contact.API.Model;
using Contact.API.Repositories.Contact;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Contact.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ContactEntity>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get([FromQuery] RequestModel model)
        {
            var contacts = await _contactRepository.Get(model);
            return Ok(contacts);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ContactEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get(string id)
        {
            var products = await _contactRepository.Get(id);
            return Ok(products);
        }

        [HttpGet("{name}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ContactEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetByName(string name)
        {
            var products = await _contactRepository.GetByName(name);
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ContactEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Create([FromBody] ContactEntity model)
        {
            await _contactRepository.Create(model);
            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ContactEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateBulk([FromBody] List<ContactEntity> model)
        {
            await _contactRepository.Create(model);
            return Ok(model);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ContactEntity), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] ContactEntity product)
        {
            return Ok(await _contactRepository.Update(product));
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _contactRepository.Delete(id));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBulk(List<string> id)
        {
            return Ok(await _contactRepository.Delete(id));
        }
    }
}
