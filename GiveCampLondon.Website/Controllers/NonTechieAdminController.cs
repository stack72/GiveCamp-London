using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiveCampLondon.Repositories;
using GiveCampLondon.Website.Models.Volunteer;

namespace GiveCampLondon.Website.Controllers
{
    public class NonTechieAdminController : Controller
    {
        private readonly INonTechVolunteerRepository _nonTechieVolunteerRepository;

        public NonTechieAdminController(INonTechVolunteerRepository nonTechieVolunteerRepository)
        {
            _nonTechieVolunteerRepository = nonTechieVolunteerRepository;
        }

        public ActionResult NonTechies()
        {
            IEnumerable<NonTechieVolunteerSummaryModel> nonTechieVolunteers = _nonTechieVolunteerRepository.FindAll()
                .Where(x => x.HasCancelled == false)
                .Select(volunteer => new NonTechieVolunteerSummaryModel
                {
                    Id = volunteer.Id,
                    LastName = volunteer.LastName,
                    FirstName = volunteer.FirstName,
                    Email = volunteer.Email,
                    PhoneNumber = volunteer.PhoneNumber,
                    TwitterHandle = volunteer.TwitterHandle
                });
            return View(nonTechieVolunteers);
        }


        public ActionResult NonTechieDetails(int id)
        {
            var nonTechie = _nonTechieVolunteerRepository.Get(id);
            nonTechie.AreasOfExpertise = _nonTechieVolunteerRepository.FindExpertiseFor(nonTechie.Id);

            return View(nonTechie);
        }

        [HttpPost]
        public ActionResult NonTechieCancellation(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _nonTechieVolunteerRepository.CancelRegistration(Id);
                    return RedirectToAction("NonTechies");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error trying to deregister user", ex);
                    return View("NonTechies");
                }
            }
            return View("NonTechies");
        }

    }
}
