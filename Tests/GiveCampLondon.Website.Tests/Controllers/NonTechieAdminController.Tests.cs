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
    }
}
