using Contact.API.Controllers;
using Contact.API.Entity;
using Contact.API.Repositories.Contact;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
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
            var result = await _contactController.Get(new Model.RequestModel(1, 10));

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetList_ActionResult_ReturnResult()
        {
            var req = new Model.RequestModel(1, 10);

            _mockRepo.Setup(x => x.Get(req)).ReturnsAsync(_contacts);

            var result = await _contactController.Get(req);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);

            var contacts = Assert.IsAssignableFrom<IEnumerable<ContactEntity>>(okObjectResult.Value);

            Assert.Equal(2, contacts.ToList().Count);
        }


        [Fact]
        public async void Get_ActionResult_ReturnOk()
        {
            string id = "602d2149e773f2a3990b4711";

            var result = await _contactController.Get(id);

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("602d2149e773f2a3990b4811")]
        public async void Get_ActionResult_ReturnResult(string id)
        {
            var contact = _contacts.Where(x => x.UUID == id).FirstOrDefault();

            _mockRepo.Setup(x => x.Get(id)).ReturnsAsync(contact);

            var result = await _contactController.Get(id);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);

            var returnContact = Assert.IsAssignableFrom<ContactEntity>(okObjectResult.Value);

            Assert.Equal(id, returnContact.UUID);
            Assert.Equal(contact.Name, returnContact.Name);

        }

        [Fact]
        public async void Get_IdIsNull_ActionResult_ReturnNotFound()
        {
            string id = null;
            var result = await _contactController.Get(id);
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async void GetByName_ActionResult_ReturnOk()
        {
            string name = "Ali";
            var result = await _contactController.GetByName(name);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetByName_IdIsNull_ActionResult_ReturnNotFound()
        {
            string name = null;
            var result = await _contactController.GetByName(name);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Create_ActionResult_ReturnOk()
        {
            var result = await _contactController.Create(new ContactEntity());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Create_ModelIsNull_ActionResult_ReturnNotFound()
        {
            ContactEntity model = null;
            var result = await _contactController.Create(model);
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData("602d2149e773f2a3990b4815")]
        public async void Create_ActionResult_ReturnResult(string id)
        {
            var contact = _contacts.FirstOrDefault();

            _mockRepo.Setup(x => x.Create(contact)).ReturnsAsync(contact);

            var result = await _contactController.Create(contact);

            _mockRepo.Verify(x => x.Create(contact), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void CreateBulk_ActionResult_ReturnOk()
        {
            var result = await _contactController.CreateBulk(_contacts);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void CreateBulk_ModelIsNull_ActionResult_ReturnNotFound()
        {
            List<ContactEntity> model = null;
            var result = await _contactController.CreateBulk(model);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void CreateBulk_ActionResult_ReturnResult()
        {
            _mockRepo.Setup(x => x.Create(_contacts)).ReturnsAsync(_contacts);

            var result = await _contactController.CreateBulk(_contacts);

            _mockRepo.Verify(x => x.Create(_contacts), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Update_ActionResult_ReturnOk()
        {
            var result = await _contactController.Update(new ContactEntity());
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Update_ModelIsNull_ActionResult_ReturnNotFound()
        {
            ContactEntity model = null;
            var result = await _contactController.Update(model);
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData("602d2149e773f2a3990b4815")]
        public async void Update_ActionResult_ReturnResult(string id)
        {
            var contact = _contacts.Where(x => x.UUID == id).FirstOrDefault();

            _mockRepo.Setup(x => x.Update(contact));

            var result = await _contactController.Update(contact);

            _mockRepo.Verify(x => x.Update(contact), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Delete_ActionResult_ReturnOk()
        {
            string id = "602d2149e773f2a3990b4711";
            var result = await _contactController.Delete(id);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Delete_ModelIsNull_ActionResult_ReturnNotFound()
        {
            var result = await _contactController.Delete(" ");
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData("602d2149e773f2a3990b4811")]
        public async void Delete_ActionResult_ReturnResult(string id)
        {
            var contact = _contacts.Where(x => x.UUID == id).First();

            _mockRepo.Setup(x => x.Delete(id));

            var result = await _contactController.Delete(id);

            _mockRepo.Verify(x => x.Delete(id), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("602d2149e773f2a3990b4815", "602d2149e773f2a3990b4811")]
        public async void DeleteBulk_ActionResult_ReturnOk(params string[] ids)
        {
            var result = await _contactController.DeleteBulk(ids.ToList());
            Assert.IsType<OkObjectResult>(result);
        }



        [Fact]
        public async void DeleteBulk_ModelIsNull_ActionResult_ReturnNotFound()
        {
            List<string> ids = null;
            var result = await _contactController.DeleteBulk(ids);
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData("602d2149e773f2a3990b4815", "602d2149e773f2a3990b4811")]
        public async void DeleteBulk_ActionResult_ReturnResult(params string[] ids)
        {
            var idList = ids.ToList();

            _mockRepo.Setup(x => x.Delete(idList));

            var result = await _contactController.DeleteBulk(idList);

            _mockRepo.Verify(x => x.Delete(idList), Times.Once);

            Assert.IsType<OkObjectResult>(result);
        }

    }
}
