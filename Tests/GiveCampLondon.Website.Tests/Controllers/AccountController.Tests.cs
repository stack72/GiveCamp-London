using GiveCampLondon.Services;
using GiveCampLondon.Website.Controllers;
using GiveCampLondon.Website.Models;
using MvcContrib.TestHelper;
using NSubstitute;
using NUnit.Framework;

namespace GiveCampLondon.Website.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        private IFormsAuthentication _formsAuth;
        public IMembershipService _membershipService;
        
        [SetUp]
        public void SetUp()
        {
            _formsAuth = Substitute.For<IFormsAuthentication>();
            _membershipService = Substitute.For<IMembershipService>();
        }

        [Test]
        public void LogonAction_Returns_View()
        {
            //Act
            var controller = new AccountController(_formsAuth, _membershipService);
            var view = controller.LogOn();

            //Assert
            view.AssertViewRendered();
        }

        [Test]
        public void LogonAction_Post_Returns_View()
        {
            //arrange
            _formsAuth.SignIn("userName", false);
            _membershipService.ValidateUser("userName", "password").Returns(true);

            //Act
            var controller = new AccountController(_formsAuth, _membershipService);
            var view = controller.LogOn("userName", "password", false, string.Empty);

            //Assert
            view.AssertActionRedirect();
        }

        [Test]
        public void LogonAction_Post_WithReturnUrl_Redirects()
        {
            //arrange
            _formsAuth.SignIn("userName", false);
            _membershipService.ValidateUser("userName", "password").Returns(true);

            //Act
            var controller = new AccountController(_formsAuth, _membershipService);
            var view = controller.LogOn("userName", "password", false, "/home/faq");

            //Assert
            view.AssertHttpRedirect();
        }

        [Test]
        public void LogonAction_Post_Passing_Empty_UserName_Returns_View()
        {
            //arrange
            _formsAuth.SignIn("userName", false);
            _membershipService.ValidateUser("userName", "password").Returns(true);

            //Act
            var controller = new AccountController(_formsAuth, _membershipService);
            var view = controller.LogOn(string.Empty, "password", false, string.Empty);

            //Assert
            view.AssertViewRendered();
        }

        [Test]
        public void LogonAction_Post_Passing_Empty_UserName_Returns_ModelError()
        {
            //arrange
            _formsAuth.SignIn("userName", false);
            _membershipService.ValidateUser("userName", "password").Returns(true);

            //Act
            var controller = new AccountController(_formsAuth, _membershipService);
            var view = controller.LogOn(string.Empty, "password", false, string.Empty);

            //Assert
            view.AssertViewRendered().ViewData.ModelState.ContainsKey("username");
        }

        [Test]
        public void LogonAction_Post_Passing_Empty_Password_Returns_View()
        {
            //arrange
            _formsAuth.SignIn("userName", false);
            _membershipService.ValidateUser("userName", "password").Returns(true);

            //Act
            var controller = new AccountController(_formsAuth, _membershipService);
            var view = controller.LogOn("userName", string.Empty, false, string.Empty);

            //Assert
            view.AssertViewRendered();
        }

        [Test]
        public void LogonAction_Post_Passing_Empty_Password_Returns_ModelError()
        {
            //arrange
            _formsAuth.SignIn("userName", false);
            _membershipService.ValidateUser("userName", "password").Returns(true);

            //Act
            var controller = new AccountController(_formsAuth, _membershipService);
            var view = controller.LogOn("userName", string.Empty, false, string.Empty);

            //Assert
            view.AssertViewRendered().ViewData.ModelState.ContainsKey("password");
        }
    
        [Test]
        public void LogOffAction_Returns_View()
        {
            //Arrange
            _formsAuth.SignOut();

            //Act
            var controller = new AccountController(_formsAuth, _membershipService);
            var result = controller.LogOff();

            //Assert
            result.AssertActionRedirect();
        }
    }
}
