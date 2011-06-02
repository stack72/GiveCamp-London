using System.Collections.Generic;
using GiveCampLondon.Repositories;
using GiveCampLondon.Website.Controllers;
using MvcContrib.TestHelper;
using NSubstitute;
using NUnit.Framework;

namespace GiveCampLondon.Website.Tests.Controllers
{
    [TestFixture]
    public class SponsorsControllerTests
    {
        [Test]
        public void IndexAction_Returns_View()
        {
            //arrange
            var sponsorRepository = Substitute.For<ISponsorRepository>();
            sponsorRepository.FindAll().Returns(new List<Sponsor>());

            //Act
            var controller = new SponsorsController(sponsorRepository);
            var result = controller.Index();

            //Assert
            result.AssertViewRendered();
        }

        [Test]
        public void IndexAction_Passes_IList_Sponsor_To_View()
        {
            //arrange
            var sponsorRepository = Substitute.For<ISponsorRepository>();
            sponsorRepository.FindAll().Returns(new List<Sponsor>());

            //Act
            var controller = new SponsorsController(sponsorRepository);
            var result = controller.Index();

            //Assert
            result.AssertViewRendered().WithViewData<IList<Sponsor>>();
        }
    }
}
