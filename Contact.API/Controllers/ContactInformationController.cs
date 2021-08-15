using Contact.API.Entity;
using Contact.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ContactInformation.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class ContactInformationController : ControllerBase
    {
        private readonly IContactInformationRepository _ContactInformationRepository;
        private readonly ILogger<ContactInformationController> _logger;

        public ContactInformationController(IContactInformationRepository ContactInformationRepository, ILogger<ContactInformationController> logger)
        {
            _ContactInformationRepository = ContactInformationRepository ?? throw new ArgumentNullException(nameof(ContactInformationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        /// <summary>
        /// A Single New Record into Contact Information
        /// </summary>
        /// <param name="id">Contact UUID</param>
        /// <param name="model">ContactInformation</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ContactInformationEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ContactInformationEntity>> Create([FromBody] string id, ContactInformationEntity model)
        {
            await _ContactInformationRepository.Create(id, model);
            return Ok(model);
        }

        /// <summary>
        /// Update A Single Record in Contact Object 
        /// </summary>
        /// <param name="id">Contact UUID</param>
        /// <param name="model">ContactInformation</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ContactInformationEntity), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] string id, ContactInformationEntity model)
        {
            return Ok(await _ContactInformationRepository.Update(id, model));
        }

        /// <summary>
        /// Delete A Single Record
        /// </summary>
        /// <param name="id">Contact UUID</param>
        /// <param name="informationId">ContactInformation UUID</param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)},{informationId:length(24)}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string id, string informationId)
        {
            return Ok(await _ContactInformationRepository.Delete(id, informationId));
        }

        /// <summary>
        /// Delete One More Than Record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="informationIds">ContactInformation Ids</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBulk(string id, List<string> informationIds)
        {
            return Ok(await _ContactInformationRepository.Delete(id, informationIds));
        }
    }
}
