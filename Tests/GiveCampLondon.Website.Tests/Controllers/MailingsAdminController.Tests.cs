using System.Collections.Generic;
using System.Web.Mvc;
using GiveCampLondon.Repositories;
using GiveCampLondon.Website.Controllers;
using MvcContrib.TestHelper;
using NSubstitute;
using NUnit.Framework;

namespace GiveCampLondon.Website.Tests.Controllers
{
    [TestFixture]
    public class MailingsAdminControllerTests
    {
        private IVolunteerRepository _volunteerRepository;
        private INonTechVolunteerRepository _nonTechieVolunteerRepository;


        [SetUp]
        public void SetUp()
        {
            _volunteerRepository = Substitute.For<IVolunteerRepository>();
            _nonTechieVolunteerRepository = Substitute.For<INonTechVolunteerRepository>();
        }

        [Test]
        public void DownloadEmailList_Action_Returns_FileContentResult()
        {
            _volunteerRepository.FindAll().ReceivedWithAnyArgs().Returns(new List<Volunteer>());
            _nonTechieVolunteerRepository.FindAll().ReceivedWithAnyArgs().Returns(new List<NonTechVolunteer>());

            var controller = new MailingsAdminController(_volunteerRepository, _nonTechieVolunteerRepository);
            var fileResult = controller.DownloadEmailList();

            fileResult.ShouldBe<FileContentResult>("Shoudl be of type FileContentResult");
        }
    }
}
