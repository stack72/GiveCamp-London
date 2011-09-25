using System.Collections.Generic;
using GiveCampLondon.Repositories;
using GiveCampLondon.Website.Controllers;
using GiveCampLondon.Website.Models.Volunteer;
using MvcContrib.TestHelper;
using NSubstitute;
using NUnit.Framework;

namespace GiveCampLondon.Website.Tests.Controllers
{
    [TestFixture]
    public class NonTechieAdminControllerTests
    {
        private INonTechVolunteerRepository _nonTechieVolunteerRepository;
 
        [SetUp]
        public void SetUp()
        {
            _nonTechieVolunteerRepository = Substitute.For<INonTechVolunteerRepository>();
        }

        [Test]
        public void NonTechies_Action_Returns_View()
        {
            _nonTechieVolunteerRepository.FindAll().ReceivedWithAnyArgs().Returns(new List<NonTechVolunteer>());

            var controller = new NonTechieAdminController(_nonTechieVolunteerRepository);
            var result = controller.NonTechies();

            result.AssertViewRendered();
        }

        [Test]
        public void NonTechies_Action_Returns_IEnumerable_NonTechieVolunteerSummaryModel_To_The_View()
        {
            _nonTechieVolunteerRepository.FindAll().ReceivedWithAnyArgs().Returns(new List<NonTechVolunteer>());

            var controller = new NonTechieAdminController(_nonTechieVolunteerRepository);
            var result = controller.NonTechies();

            result.AssertViewRendered().WithViewData<IEnumerable<NonTechieVolunteerSummaryModel>>();
        }

        [Test]
        public void NonTechieDetail_Action_Returns_View()
        {
            _nonTechieVolunteerRepository.Get(0).ReceivedWithAnyArgs().Returns(new NonTechVolunteer());

            var controller = new NonTechieAdminController(_nonTechieVolunteerRepository);
            var result = controller.NonTechieDetails(1);

            result.AssertViewRendered();
        }

        [Test]
        public void NonTechieDetail_Action_Returns_NonTechVolunteer_To_The_View()
        {
            _nonTechieVolunteerRepository.Get(0).ReceivedWithAnyArgs().Returns(new NonTechVolunteer());

            var controller = new NonTechieAdminController(_nonTechieVolunteerRepository);
            var result = controller.NonTechieDetails(1);

            result.AssertViewRendered().WithViewData<NonTechVolunteer>();
        }
    
        [Test]
        public void NonTechieCancellation_Action_Redirects_To_Techies_View_When_Invalid_Model()
        {
            var controller = new NonTechieAdminController(_nonTechieVolunteerRepository);
            controller.ModelState.AddModelError("Error", "Simple Error For test");

            var result = controller.NonTechieCancellation(1);

            Assert.That(result.AssertViewRendered().ViewName == "NonTechies");
        }

        public void NonTechieCancellation_Action_Redirects_To_Techies_View_When_Cancellation_Fails()
        {
            //_nonTechieVolunteerRepository.CancelRegistration(1).

            //var controller = new NonTechieAdminController(_nonTechieVolunteerRepository);
            //controller.ModelState.AddModelError("Error", "Simple Error For test");

            //var result = controller.NonTechieCancellation(1);

            //Assert.That(result.AssertViewRendered().ViewName == "NonTechies");
        }
    }
}
