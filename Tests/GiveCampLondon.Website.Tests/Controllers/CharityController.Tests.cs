using GiveCampLondon.Repositories;
using GiveCampLondon.Services;
using GiveCampLondon.Website.Controllers;
using GiveCampLondon.Website.Models.Charity;
using MvcContrib.TestHelper;
using NSubstitute;
using NUnit.Framework;

namespace GiveCampLondon.Website.Tests.Controllers
{
    [TestFixture]
    public class CharityControllerTests
    {
        private  ICharityRepository _charityRepository;
        private  INotificationService _notificationService;

        [SetUp]
        public void SetUp()
        {
            _charityRepository = Substitute.For<ICharityRepository>();
            _notificationService = Substitute.For<INotificationService>();
        }

        [Test]
        public void IndexAction_Returns_View()
        {
            //Act
            var controller = new CharityController(_charityRepository, _notificationService);
            var result = controller.Index();

            //Assert
            result.AssertViewRendered();
        }

        [Test]
        public void SignUpAction_Returns_View()
        {
            //Act
            var controller = new CharityController(_charityRepository, _notificationService);
            var result = controller.SignUp();

            //Assert
            result.AssertViewRendered();
        }

        [Test]
        public void SignUpAction_Post_Redirects()
        {
            //Arrange
            var model = new SignUpViewModel();
            _charityRepository.Save(new Charity());
            _notificationService.SendNotification(string.Empty, VolunteerNotificationTemplate.WelcomeVolunteer).Returns(true);

            //Act
            var controller = new CharityController(_charityRepository, _notificationService);
            var result = controller.SignUp(model);

            //Assert
            result.AssertActionRedirect();
        }

        [Test]
        public void SignUpAction_Post__With_ModelError_Returns_View()
        {
            //Arrange
            var model = new SignUpViewModel();
            _charityRepository.Save(new Charity());
            _notificationService.SendNotification(string.Empty, VolunteerNotificationTemplate.WelcomeVolunteer).Returns(true);

            //Act
            var controller = new CharityController(_charityRepository, _notificationService);
            controller.ModelState.AddModelError("An Error", "Message");
            var result = controller.SignUp(model);

            //Assert
            result.AssertViewRendered();
        }

        [Test]
        public void ThankYouAction_Returns_View()
        {
            //Act
            var controller = new CharityController(_charityRepository, _notificationService);
            var result = controller.ThankYou();

            //Assert
            result.AssertViewRendered();
        }
    }
}
