using System;
using NUnit.Framework;
using GiveCampLondon.Website.Areas.TeamAdministration.Controllers;
using Rhino.Mocks;
using StructureMap.AutoMocking;
using GiveCampLondon.Repositories;
using System.Web.Mvc;
using System.Collections.Generic;
using GiveCampLondon.Website.Areas.TeamAdministration.Models.Home;

namespace GiveCampLondon.Tests.UnitTests.Controllers.Areas.TeamAdministration
{
    [TestFixture]
    public class HomeControllerTests
    {
        [SetUp]
        public void SetUp()
        {
            _ctrl = new RhinoAutoMocker<HomeController>();
        }

        private RhinoAutoMocker<HomeController> _ctrl;

        [Test]
        public void Index_NoTeams_ReturnsNoTeamsView()
        {
            _ctrl.Get<ITeamRepository>().Stub(t => t.GetAll())
                .Return(new List<Team>());

            var result = _ctrl.ClassUnderTest.Index() as ViewResult;
            Assert.IsNotNull(result, "Expected a ViewResult.");
            Assert.AreEqual("noteams", result.ViewName.ToLower(), "Wrong view!");
        }

        [Test]
        public void Index_Teams_ReturnsViewWithList()
        {
            _ctrl.Get<ITeamRepository>().Stub(t => t.GetAll())
                .Return(new List<Team>
                {
                    new Team { Id = 1, Name = "1" },
                    new Team { Id = 2, Name = "2" },
                    new Team { Id = 3, Name = "3" }
                });

            var result =_ctrl.ClassUnderTest.Index() as ViewResult;
            Assert.IsNotNull(result, "Expected a ViewResult.");

            var model = result.ViewBag.Teams as List<Team>;
            Assert.AreEqual(3, model.Count, "Wrong number of teams returned.");
        }

        [Test]
        public void Edit_ValidId_ReturnsView()
        {
            _ctrl.Get<ITeamRepository>().Stub(t => t.Get(7))
                .Return(new Team { Id = 7, Name = "The Flaming Monkeys" });

            _ctrl.Get<IVolunteerRepository>().Stub(v => v.GetForTeam(Arg<int>.Is.Anything))
                .Return(new List<Volunteer>
                {
                    new Volunteer { Id = 1, FirstName = "Bob" },
                    new Volunteer { Id = 2, FirstName = "Fred" }
                });

            _ctrl.Get<IVolunteerRepository>().Stub(v => v.GetAllNotInTeam(Arg<int>.Is.Anything))
            .Return(new List<Volunteer>
                        {
                            new Volunteer { Id = 3, FirstName = "Joe" }
                        });

            var result = _ctrl.ClassUnderTest.Edit(7) as ViewResult;
            Assert.IsNotNull(result, "Expected a ViewResult.");

            var editModel = result.Model as EditViewModel;
            Assert.IsNotNull(editModel, "Expected and EditViewModel");
            Assert.IsNotNull(editModel.Team, "The model did not have a team.");
            Assert.AreEqual("The Flaming Monkeys", editModel.Team.Name, "The team was not properly returned.");
            Assert.AreEqual(2, editModel.Volunteers.Count, "Wrong number of volunteers returned.");
            Assert.AreEqual(1, editModel.OtherVolunteers.Count, "Wrong number of other volunteers.");
        }

        [Test]
        public void GetVolunteersInTeam_TeamWithVolunteers_ReturnsPartialViewOfList()
        {
            _ctrl.Get<IVolunteerRepository>().Stub(v => v.GetForTeam(Arg<int>.Is.Anything))
                .Return(new List<Volunteer>
                            {
                                new Volunteer { Id = 1, FirstName = "Bob" },
                                new Volunteer { Id = 2, FirstName = "Fred" }
                            });

            var result = _ctrl.ClassUnderTest.GetVolunteersInTeam(4) as PartialViewResult;
            Assert.IsNotNull(result, "Expected a PartialViewResult.");

            var model = result.Model as List<Volunteer>;
            Assert.IsNotNull(model, "Expected List<Volunteer>");
            Assert.AreEqual(2, model.Count, "Wrong number of volunteers.");
        }

        [Test]
        public void GetVolunteersNotInTeam_ReturnsPartialViewOfList()
        {
            _ctrl.Get<IVolunteerRepository>().Stub(v => v.GetAllNotInTeam(Arg<int>.Is.Anything))
            .Return(new List<Volunteer>
                        {
                            new Volunteer { Id = 3, FirstName = "Joe" }
                        });

            var result = _ctrl.ClassUnderTest.GetVolunteersNotInTeam(4) as PartialViewResult;
            Assert.IsNotNull(result, "Expected a PartialViewResult.");

            var model = result.Model as List<Volunteer>;
            Assert.IsNotNull(model, "Expected List<Volunteer>");
            Assert.AreEqual(1, model.Count, "Wrong number of volunteers.");
        }

        [Test]
        public void AddVolunteerToTeam_Adds()
        {
            _ctrl.Get<IVolunteerRepository>().Stub(m => m.Get(678))
                .Return(new Volunteer());

            var result =  _ctrl.ClassUnderTest.AddVolunteerToTeam(1, 678) as JsonResult;
            Assert.IsNotNull(result, "Expect a json result.");

            _ctrl.Get<IVolunteerRepository>().AssertWasCalled(m => m.Get(678));
            _ctrl.Get<IVolunteerRepository>().AssertWasCalled(m => m.Save(Arg<Volunteer>.Is.Anything));
        }

        [Test]
        public void RemoveVolunteerFromTeam_Removes()
        {
            _ctrl.Get<IVolunteerRepository>().Stub(m => m.Get(678))
                .Return(new Volunteer());

            var result = _ctrl.ClassUnderTest.RemoveVolunteerFromTeam(1, 678) as JsonResult;
            Assert.IsNotNull(result, "Expect a json result.");

            _ctrl.Get<IVolunteerRepository>().AssertWasCalled(m => m.Get(678));
            _ctrl.Get<IVolunteerRepository>().AssertWasCalled(m => m.Save(Arg<Volunteer>.Is.Anything));
        }
    }
}
