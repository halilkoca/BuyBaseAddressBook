using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Report.API.Controllers;
using Report.API.Entity;
using System.Collections.Generic;
using Xunit;

namespace Report.API.Test
{
    public class ReportControllerTest
    {
        private readonly Mock<IDistributedCache> _mock;
        private readonly ReportController _reportController;
        private List<LocationReportEntity> _entity;

        public ReportControllerTest()
        {
            _mock = new Mock<IDistributedCache>();
            _reportController = new ReportController(_mock.Object);
            _entity = new List<LocationReportEntity>
            {
                new LocationReportEntity
                {
                    Location = "İSTANBUL",
                    PeopleCount = 1,
                    PhoneNumberCount = 1
                }
            };
        }

        [Fact]
        public async void GetList_ActionResult_ReturnOk()
        {
            var result = await _reportController.Location();

            Assert.IsType<NotFoundResult>(result);
        }

    }
}
