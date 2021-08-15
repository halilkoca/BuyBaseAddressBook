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
        private readonly ILogger<ContactController> _logger;

        public ContactController(IContactRepository contactRepository, ILogger<ContactController> logger)
        {
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ContactEntity>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ContactEntity>>> Get([FromQuery] RequestModel model)
        {
            var products = await _contactRepository.Get(model);
            return Ok(products);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ContactEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ContactEntity>>> Get(string id)
        {
            var products = await _contactRepository.Get(id);
            return Ok(products);
        }

        [HttpGet("{name}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ContactEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ContactEntity>>> GetByName(string name)
        {
            var products = await _contactRepository.GetByName(name);
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ContactEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ContactEntity>> Create([FromBody] ContactEntity model)
        {
            await _contactRepository.Create(model);
            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ContactEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ContactEntity>> CreateBulk([FromBody] List<ContactEntity> model)
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
