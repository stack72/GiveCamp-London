using GiveCampLondon.Repositories;
using GiveCampLondon.Services;
using GiveCampLondon.Website.Controllers;
using GiveCampLondon.Website.Helpers;
using MvcContrib.TestHelper;
using NSubstitute;
using NUnit.Framework;

namespace GiveCampLondon.Website.Tests.Controllers
{
    [TestFixture]
    public class NonTechVolunteerControllerTests
    {
        private IWaitListHelper _waitListHelper;
        private INonTechVolunteerRepository _volunteerRepository;
        private IExpertiseRepository _expertiseRepository;
        private INotificationService _notificationService;

        [SetUp]
        public void Setup()
        {
            _waitListHelper = Substitute.For<IWaitListHelper>();
            _volunteerRepository = Substitute.For<INonTechVolunteerRepository>();
            _notificationService = Substitute.For<INotificationService>();
            _expertiseRepository = Substitute.For<IExpertiseRepository>();
        }

        [Test]
        public void ThankYou_Action_Returns_View()
        {
            //Act
            var controller = new NonTechVolunteerController(_waitListHelper, _volunteerRepository, _expertiseRepository, _notificationService);
            var result = controller.ThankYou();

            //assert
            result.AssertViewRendered();
        }
    }
}
