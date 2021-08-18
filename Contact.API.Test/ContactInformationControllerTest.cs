using Contact.API.Entity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;
using ContactInformation.API.Controllers;
using Contact.API.Repositories.ContactInformation;
using Contact.API.Repositories.Report;
using MassTransit;
using AutoMapper;
using System.Linq;

namespace Contact.API.Test
{
    public class ContactInformationControllerTest
    {
        private readonly Mock<IContactInformationRepository> _mockRepo;
        private readonly Mock<IReportRepository> _mockReport;
        private readonly Mock<IPublishEndpoint> _mockPublish;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ContactInformationController _contactInformationController;

        private List<ContactEntity> _contacts;

        public ContactInformationControllerTest()
        {
            _mockRepo = new Mock<IContactInformationRepository>();
            _mockReport = new Mock<IReportRepository>();
            _mockPublish = new Mock<IPublishEndpoint>();
            _mockMapper = new Mock<IMapper>();
            _contactInformationController = new ContactInformationController(_mockRepo.Object, _mockReport.Object, _mockPublish.Object, _mockMapper.Object);

            _contacts = new List<ContactEntity>
            {
                new ContactEntity()
                {
                    UUID = "602d2149e773f2a3990b4811",
                    Name = "Kamil",
                    LastName = "Dalda",
                    Firm = "SST",
                    CreatedDate = System.DateTime.UtcNow,
                    ContactInformations = new List<ContactInformationEntity>
                    {
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4812",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.PhoneNumber,
                            Value = "+905328320230"
                        },
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4813",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.EmailAddress,
                            Value = "kamildalda@gmail.com"
                        },
                        new ContactInformationEntity
                        {
                            UUID = "602d2149e773f2a3990b4814",
                            CreatedDate = System.DateTime.UtcNow,
                            Type = InformationType.Location,
                            Value = "İstanbul"
                        }
                    }
                },
                new ContactEntity()
                {
                    UUID = "602d2149e773f2a3990b4815",
                    Name = "Kamile",
                    LastName = "Acarlı",
                    Firm = "SST",
                    CreatedDate = System.DateTime.UtcNow,
                },
            };
        }

        [Theory]
        [InlineData("602d2149e773f2a3990b4811")]
        public async void Create_ActionResult_ReturnOk(string id)
        {
            var result = await _contactInformationController.Create(id, new ContactInformationEntity());

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("602d2149e773f2a3990b4811", "602d2149e773f2a3990b4812")]
        public async void Update_ActionResult_ReturnOk(params string[] ids)
        {
            var contactInformation = _contacts.Where(x => x.UUID == ids[0]).FirstOrDefault()
                .ContactInformations.FirstOrDefault();
            var result = await _contactInformationController.Update(ids[0], contactInformation);
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("602d2149e773f2a3990b4811", "602d2149e773f2a3990b4812")]
        public async void Delete_ActionResult_ReturnOk(params string[] ids)
        {
            var result = await _contactInformationController.Delete(ids[0], ids[1]);

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("602d2149e773f2a3990b4811", "602d2149e773f2a3990b4812", "602d2149e773f2a3990b4813")]
        public async void DeleteBulk_ActionResult_ReturnOk(params string[] ids)
        {
            var result = await _contactInformationController.DeleteBulk(ids[0], new List<string>() { ids[1], ids[2] });

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
