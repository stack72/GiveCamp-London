using System;
using System.Web.Mvc;
using GiveCampStarterKit.Repositories;
using GiveCampStarterKit.Website.Areas.TeamAdministration.Models.Home;
using GiveCampStarterKit.Website.Controllers;

namespace GiveCampStarterKit.Website.Areas.TeamAdministration.Controllers
{
    public class HomeController: BaseController
    {
        public HomeController(ITeamRepository teamRepository, IVolunteerRepository volunteerRepository, ISettingRepository settingRepository)
            :base(settingRepository)
        {
            _teamRepository = teamRepository;
            _volunteerRepository = volunteerRepository;
        }

        private ITeamRepository _teamRepository;
        private IVolunteerRepository _volunteerRepository;

        public ActionResult Index()
        {
            var teams = _teamRepository.GetAll();

            if (teams.Count == 0)
                return View("NoTeams");

            ViewBag.Teams = teams;

        	
            return View();
        }

        public ActionResult Edit(int? id)
        {
            var team = _teamRepository.Get(id.Value);
            var model = new EditViewModel();
            model.Team = team;
            model.Volunteers = _volunteerRepository.GetForTeam(id.Value);
            model.OtherVolunteers = _volunteerRepository.GetAllNotInTeam(id.Value);

            return View(model);
        }
        public ActionResult CreateTeam(string teamName)
        {
            _teamRepository.Save(new Team{Name = teamName});

            return RedirectToAction("Index");
        }

        public ActionResult DeleteTeam(int id)
        {
            var team = _teamRepository.Get(id);
            var volunteers = _volunteerRepository.GetForTeam(id);

            foreach (var volunteer in volunteers)
            {
                volunteer.TeamId = null;
                _volunteerRepository.Save(volunteer);
            }

            _teamRepository.Delete(team);
            return RedirectToAction("Index");
        }
        public ActionResult GetVolunteersInTeam(int? teamId)
        {
            var volunteers = _volunteerRepository.GetForTeam(teamId.Value);
            if (volunteers.Count > 0)
                return PartialView("CurrentVolunteersTable", volunteers);
            else
                return Content("<p>This team has no volunteers</p>");
        }

        public ActionResult GetVolunteersNotInTeam(int? teamId)
        {
            var volunteers = _volunteerRepository.GetAllNotInTeam(teamId.Value);
            if (volunteers.Count > 0)
                return PartialView("VolunteersToAddTable", volunteers);
            else
                return Content("<p>No volunteers not assigned to this team.</p>");
        }

        [HttpPost]
        public ActionResult AddVolunteerToTeam(int? teamId, int? volunteerId)
        {
            var volunteer = _volunteerRepository.Get(volunteerId.Value);
            volunteer.TeamId = teamId.Value;
            _volunteerRepository.Save(volunteer);

            return Json(true);
        }

        [HttpPost]
        public ActionResult RemoveVolunteerFromTeam(int? teamId, int? volunteerId)
        {
            var volunteer = _volunteerRepository.Get(volunteerId.Value);
            volunteer.TeamId = null;
            _volunteerRepository.Save(volunteer);

            return Json(true);
        }

        [HttpPost]
        public ActionResult ChangeTeamName(int? teamId, string teamName)
        {
            var team = _teamRepository.Get(teamId.Value);
            team.Name = teamName;
            _teamRepository.Save(team);

            return Json(true);
        }

    }
}
