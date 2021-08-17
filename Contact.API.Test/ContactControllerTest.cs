using Contact.API.Controllers;
using Contact.API.Entity;
using Contact.API.Repositories.Contact;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Contact.API.Test
{
    public class ContactControllerTest
    {
        private readonly Mock<IContactRepository> _mockRepo;
        private readonly ContactController _contactController;

        private List<ContactEntity> _contacts;

        public ContactControllerTest()
        {
            _mockRepo = new Mock<IContactRepository>();
            _contactController = new ContactController(_mockRepo.Object);

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

        [Fact]
        public async void GetList_ActionResult_ReturnOk()
        {
            var result = await _contactController.Get(new Model.RequestModel());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Get_ActionResult_ReturnOk()
        {
            var result = await _contactController.Get("");

            Assert.IsType<OkObjectResult>(result);
        }



    }
}
