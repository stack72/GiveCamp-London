using System.Collections.Generic;
using GiveCampLondon.Repositories;
using GiveCampLondon.Website.Controllers;
using GiveCampLondon.Website.Helpers;
using MvcContrib.TestHelper;
using NSubstitute;
using NUnit.Framework;

namespace GiveCampLondon.Website.Tests.Controllers
{
    [TestFixture]
    public class ContentControllerTests
    {
        private ISponsorRepository _sponsorRepository;
        private IWaitListHelper _waitListHelper;

        [SetUp]
        public void Setup()
        {
            _sponsorRepository = Substitute.For<ISponsorRepository>();
            _waitListHelper = Substitute.For<IWaitListHelper>();
        }

        [Test]
        public void EventFullBanner_Action_Returns_View()
        {
            //Arrange
            _waitListHelper.SetWaitListStatus().Returns(true);

            //Act
            var controller = new ContentController(_sponsorRepository, _waitListHelper);
            var result = controller.EventFullBanner();
            
            //Assert
            result.AssertPartialViewRendered();
        }
    
        [Test]
        public void EventFullBanner_Action_Sets_WaitList_Property_In_ViewBag()
        {
            //Arrange
            _waitListHelper.SetWaitListStatus().Returns(true);

            //Act
            var controller = new ContentController(_sponsorRepository, _waitListHelper);
            var result = controller.EventFullBanner();

            //Assert
            Assert.IsNotNull(result.AssertPartialViewRendered().ViewBag.IsEventFull);
        }

        [Test]
        public void RotatorContent_Action_Returns_View()
        {
            //Arrange
            _sponsorRepository.FindAll().ReceivedWithAnyArgs().Returns(new List<Sponsor>());

            //Act
            var controller = new ContentController(_sponsorRepository, _waitListHelper);
            var result = controller.RotatorContent();

            //Assert
            result.AssertPartialViewRendered();
        }
    
        [Test]
        public void RotatorContent_Action_Returns_IList_Sponsor_To_The_View()
        {
            //Arrange
            _sponsorRepository.FindAll().ReceivedWithAnyArgs().Returns(new List<Sponsor>());

            //Act
            var controller = new ContentController(_sponsorRepository, _waitListHelper);
            var result = controller.RotatorContent();

            //Assert
            result.AssertPartialViewRendered().WithViewData<IList<Sponsor>>();
        }
    }
}
