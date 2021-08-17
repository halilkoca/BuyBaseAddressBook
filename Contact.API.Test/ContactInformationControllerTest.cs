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
        }

        [Fact]
        public async void Create_ActionResult_ReturnOk()
        {
            var result = await _contactInformationController.Create("", new ContactInformationEntity());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Update_ActionResult_ReturnOk()
        {
            var result = await _contactInformationController.Update("", new ContactInformationEntity());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Delete_ActionResult_ReturnOk()
        {
            var result = await _contactInformationController.Delete("", "");

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void DeleteBulk_ActionResult_ReturnOk()
        {
            var result = await _contactInformationController.DeleteBulk("", new List<string>());

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
