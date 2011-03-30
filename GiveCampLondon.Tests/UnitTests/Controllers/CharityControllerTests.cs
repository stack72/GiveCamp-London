using System;
using System.Web.Security;
using GiveCampLondon.Services;
using NUnit.Framework;
using GiveCampLondon.Website.Controllers;
using GiveCampLondon.Repositories;
using StructureMap.AutoMocking;
using System.Web.Mvc;
using GiveCampLondon.Website.Models.Charity;
using Rhino.Mocks;
using System.Web.Routing;
using System.Security.Principal;
using System.Web;

namespace GiveCampLondon.Tests.UnitTests.Controllers
{
    [TestFixture]
    public class CharityControllerTests
    {
        private RhinoAutoMocker<CharityController> _controller;

        [SetUp]
        public void SetUp()
        {
            MembershipUser user = new MembershipUser("AspNetSqlMembershipProvider", "TEST1", Guid.NewGuid(), "", "", "", true, false,
                                                     DateTime.Now,
                                                     DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
            _controller = new RhinoAutoMocker<CharityController>();
            _controller.Get<IMembershipService>().Stub(ms => ms.GetUserByName(Arg<string>.Is.Anything)).Return(
                user);


            var userMock = new RhinoAutoMocker<IPrincipal>();
            userMock.Get<IPrincipal>().Stub(u => u.Identity.IsAuthenticated)
                .Return(true);

            var contextMock = new RhinoAutoMocker<HttpContextBase>();
            contextMock.Get<HttpContextBase>().Stub(ctx => ctx.User)
                .Return(userMock.ClassUnderTest);

            var controllerContextMock = new RhinoAutoMocker<ControllerContext>();
            controllerContextMock.Get<ControllerContext>().Stub(con => con.HttpContext)
                .Return(contextMock.ClassUnderTest);

            _controller.ClassUnderTest.ControllerContext = controllerContextMock.ClassUnderTest;
        }


        [Test]
        public void SignUp_InvalidViewModelPosted_ReturnsView()
        {
            _controller.ClassUnderTest.ModelState.AddModelError("Name", "required, yo");

            var vm = new SignUpViewModel();

            var result = _controller.ClassUnderTest.SignUp(vm) as ViewResult;
            Assert.IsNotNull(result, "Expected a ViewResult.");
        }

        [Test]
        public void SignUp_ValidViewModelPosted_SaveAndRedirect()
        {
            var vm = new SignUpViewModel();

            var result =  _controller.ClassUnderTest.SignUp(vm) as RedirectToRouteResult;
            Assert.IsNotNull(result, "Expected RedirectToRouteResult.");
            Assert.AreEqual("thankyou", result.RouteValues["action"].ToString().ToLower());

            _controller.Get<ICharityRepository>().AssertWasCalled(r => r.Save(Arg<Charity>.Is.Anything));
        }
    }
}
