using AutoMapper;
using Contact.API.Entity;
using Contact.API.Repositories.ContactInformation;
using Contact.API.Repositories.Report;
using EventBus.Messages.Events;
using MassTransit;
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
        private readonly IContactInformationRepository _contactInformationRepository;
        private readonly IReportRepository _reportRepository;
        private readonly ILogger<ContactInformationController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public ContactInformationController(IContactInformationRepository contactInformationRepository, IReportRepository reportRepository,
            ILogger<ContactInformationController> logger, IPublishEndpoint publishEndpoint, IMapper mapper
            )
        {
            _contactInformationRepository = contactInformationRepository ?? throw new ArgumentNullException(nameof(contactInformationRepository));
            _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// A Single New Record into Contact Information
        /// </summary>
        /// <param name="id">Contact UUID</param>
        /// <param name="model">ContactInformation</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ContactInformationEntity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ContactInformationEntity>> Create([FromQuery] string id, [FromBody] ContactInformationEntity model)
        {
            await _contactInformationRepository.Create(id, model);

            // publish event 
            var result = await _reportRepository.GenerateLocationReport();
            var locationReport = _mapper.Map<List<LocationReportEvent>>(result);
            await _publishEndpoint.Publish(new LocationReportEventList { LocationReportEvents = locationReport });

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
        public async Task<IActionResult> Update([FromQuery] string id, [FromBody] ContactInformationEntity model)
        {
            return Ok(await _contactInformationRepository.Update(id, model));
        }

        /// <summary>
        /// Delete A Single Record
        /// </summary>
        /// <param name="id">Contact UUID</param>
        /// <param name="informationId">ContactInformation UUID</param>
        /// <returns></returns>
        [HttpDelete("{id:length(24)},{informationId:length(24)}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromQuery] string id, [FromQuery] string informationId)
        {
            return Ok(await _contactInformationRepository.Delete(id, informationId));
        }

        /// <summary>
        /// Delete One More Than Record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="informationIds">ContactInformation Ids</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBulk([FromQuery] string id, [FromBody] List<string> informationIds)
        {
            return Ok(await _contactInformationRepository.Delete(id, informationIds));
        }
    }
}
