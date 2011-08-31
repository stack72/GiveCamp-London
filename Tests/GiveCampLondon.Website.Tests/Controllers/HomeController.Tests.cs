using GiveCampLondon.Services;
using GiveCampLondon.Website.Controllers;
using MvcContrib.TestHelper;
using NSubstitute;
using NUnit.Framework;

namespace GiveCampLondon.Website.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private INotificationService _notificationService;
        [SetUp]
        public void SetUp()
        {
            _notificationService = Substitute.For<INotificationService>();
        }

        [Test]
        public void IndexAction_Returns_View()
        {
            //Act
            var controller = new HomeController(_notificationService);
            var result = controller.Index();

            //Assert
            result.AssertViewRendered();
        }

        [Test]
        public void ScheduleAction_Returns_View()
        {
            //Act
            var controller = new HomeController(_notificationService);
            var result = controller.Schedule();

            //Assert
            result.AssertViewRendered();
        }

        [Test]
        public void LocationAction_Returns_View()
        {
            //Act
            var controller = new HomeController(_notificationService);
            var result = controller.Location();

            //Assert
            result.AssertViewRendered();
        }

        [Test]
        public void ContactUsAction_Returns_View()
        {
            //Act
            var controller = new HomeController(_notificationService);
            var result = controller.ContactUs();

            //Assert
            result.AssertViewRendered();
        }
    }
}
