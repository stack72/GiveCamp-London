using System;
using System.Collections.Generic;
using GiveCampLondon.Repositories;
using GiveCampLondon.Services;
using GiveCampLondon.Website.Controllers;
using GiveCampLondon.Website.Helpers;
using GiveCampLondon.Website.Models.Volunteer;
using MvcContrib.TestHelper;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;

namespace GiveCampLondon.Website.Tests.Controllers
{
    [TestFixture]
    public class VolunteerControllerTests
    {
        private IWaitListHelper _waitListHelper;
        private IVolunteerRepository _volunteerRepository;
        private IJobRoleRepository _jobRoleRepository;
        private INotificationService _notificationService;
        private ITechnologyRepository _technologyRepository;
        private IExperienceLevelRepository _experienceLevelRepository;
        
        [SetUp]
        public void Setup()
        {
            _waitListHelper = Substitute.For<IWaitListHelper>();
            _volunteerRepository = Substitute.For<IVolunteerRepository>();
            _jobRoleRepository = Substitute.For<IJobRoleRepository>();
            _notificationService = Substitute.For<INotificationService>();
            _technologyRepository = Substitute.For<ITechnologyRepository>();
            _experienceLevelRepository = Substitute.For<IExperienceLevelRepository>();
        }

        [Test]
        public void Index_Action_Returns_View()
        {
            //Act
            var controller = new VolunteerController(_waitListHelper, _volunteerRepository, _jobRoleRepository, _notificationService, _technologyRepository, _experienceLevelRepository);
            var result = controller.Index();

            //Assert
            result.AssertViewRendered();
        }

        [Test]
        public void SignUp_Action_Returns_View()
        {
            //Arrange
            _jobRoleRepository.FindAll().ReceivedWithAnyArgs().Returns(new List<JobRole>());
            _technologyRepository.FindAll().ReceivedWithAnyArgs().Returns(new List<Technology>());
            _experienceLevelRepository.FindAll().ReceivedWithAnyArgs().Returns(new List<ExperienceLevel>());

            //Act
            var controller = new VolunteerController(_waitListHelper, _volunteerRepository, _jobRoleRepository, _notificationService, _technologyRepository, _experienceLevelRepository);
            var result = controller.SignUp();

            //assert
            result.AssertViewRendered();
        }

        [Test]
        public void ThankYou_Action_Returns_View()
        {
            //Act
            var controller = new VolunteerController(_waitListHelper, _volunteerRepository, _jobRoleRepository, _notificationService, _technologyRepository, _experienceLevelRepository);
            var result = controller.ThankYou();

            //Assert
            result.AssertViewRendered();
        }
    }
}
