using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GiveCampLondon.Repositories;
using GiveCampLondon.Website.Models.Volunteer;

namespace GiveCampLondon.Website.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class VolunteerAdminController : Controller
    {
        private readonly IExperienceLevelRepository _xpLevelRepository;
        private readonly IVolunteerRepository _volunteerRepository;

        public VolunteerAdminController(IExperienceLevelRepository xpLevelRepository, IVolunteerRepository volunteerRepository)
        {
            _xpLevelRepository = xpLevelRepository;
            _volunteerRepository = volunteerRepository;
        }
        public ActionResult TechieDetails(int id)
        {
            var volunteer = _volunteerRepository.Get(id);

            volunteer.JobRoles = _volunteerRepository.FindJobRolesFor(volunteer.Id);
            volunteer.Technologies = _volunteerRepository.FindTechnologiesFor(volunteer.Id);
            volunteer.ExperienceLevel = _xpLevelRepository.GetForVolunteerId(volunteer.Id);
            return View(volunteer);
        }

        public ActionResult Techies()
        {
            var volunteers = _volunteerRepository.FindAll();
            var techies = BuildTechiesViewModel(volunteers);

            return View(techies);
        }

        [HttpPost]
        public ActionResult TechieCancellation(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _volunteerRepository.CancelRegistration(Id);
                    return RedirectToAction("Techies");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error trying to deregister user", ex);
                    return View("Techies");
                }
            }
            return View("Techies");
        }

        private static TechieSummary BuildTechiesViewModel(IList<Volunteer> volunteers)
        {
            var techies = new TechieSummary
            {
                Volunteers = from volunteer in volunteers
                                 .Where(x => x.HasCancelled == false)
                             select new VolunteerSummaryModel
                             {
                                 Email = volunteer.Email,
                                 FirstName = volunteer.FirstName,
                                 Id = volunteer.Id,
                                 LastName = volunteer.LastName,
                                 PhoneNumber = volunteer.PhoneNumber,
                                 TeamName = volunteer.TeamName,
                                 TwitterHandle = volunteer.TwitterHandle
                             },
                TotalSignups = volunteers.Count,
                TotalCancellations = volunteers.Where(x => x.HasCancelled).Count(),
                TotalStillRegistered = volunteers.Where(x => x.HasCancelled == false).Count(),
                OnWaitListVolunteers = (from count in volunteers
                                         .Where(x => x.IsOnWaitList)
                                         .Where(x => x.HasCancelled == false)
                                        select count).Count()
            };
            techies.RegisteredTechies = techies.TotalSignups - techies.OnWaitListVolunteers;
            return techies;
        }

    }
}
