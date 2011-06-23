using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using GiveCampLondon.Repositories;
using GiveCampLondon.Services;
using GiveCampLondon.Website.Helpers;
using GiveCampLondon.Website.Models;
using GiveCampLondon.Website.Models.Volunteer;

namespace GiveCampLondon.Website.Controllers
{
    public class NonTechVolunteerController : Controller
    {
        public NonTechVolunteerController(IWaitListHelper waitListHelper,
            INonTechVolunteerRepository volunteerRepository,
            IExpertiseRepository expertiseRepository,
            INotificationService notificationService)
        {
            _waitListHelper = waitListHelper;
            _expertiseRepository = expertiseRepository;
            _volunteerRepository = volunteerRepository;
            _notificationService = notificationService;
        }

        private readonly IWaitListHelper _waitListHelper;
        private readonly INonTechVolunteerRepository _volunteerRepository;
        private readonly IExpertiseRepository _expertiseRepository;
        private readonly INotificationService _notificationService;

        public ActionResult SignUp()
        {
            InitializeViewBag(null);
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(NonTechVolunteerViewModel model, FormCollection collection)
        {
            var selectedExpertiseIds = GetSelectedValues(collection, "areaseOfExpertise", true, "Please select at least one area of expertise.");

            if (!ModelState.IsValid)
            {
                InitializeViewBag(model);
                return View();
            }

            if (SaveVolunteer(model, selectedExpertiseIds))
            {
                return RedirectToAction("ThankYou");
            }

            ModelState.AddModelError("SignUpStatus", "Could not sign you up - please try again");
            InitializeViewBag(model);
            return View();
        }

        public ActionResult ThankYou()
        {
            return View();
        }

        private bool SaveVolunteer(NonTechVolunteerViewModel model, IEnumerable<int> selectedExpertiseLevels)
        {
            if (ModelState.IsValid)
            {
                var volunteer = CreateVolunteer(model, selectedExpertiseLevels);
                _volunteerRepository.Save(volunteer);
                _notificationService.SendNotification(model.Email,
                                                      volunteer.IsOnWaitList
                                                          ? VolunteerNotificationTemplate.WelcomeWaitingVolunteer
                                                          : VolunteerNotificationTemplate.WelcomeVolunteer);
                return true;
            }
            return false;
        }

        private NonTechVolunteer CreateVolunteer(NonTechVolunteerViewModel model, IEnumerable<int> selectedExpertiseLevels)
        {
            var volunteer = model.MapToNonTechVolunteerModel();
            volunteer.IsOnWaitList = _waitListHelper.SetWaitListStatus();
            foreach (var expertiseId in selectedExpertiseLevels)
            {
                volunteer.AreasOfExpertise.Add(_expertiseRepository.Get(expertiseId));
            }

            return volunteer;
        }

        private IEnumerable<int> GetSelectedValues(FormCollection collection, string formObjectName, bool isRequired, string isRequiredErrorMessage)
        {
            var list = new List<int>();
            if (collection[formObjectName] != null)
            {
                var jobRoles = collection[formObjectName].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                list.AddRange(jobRoles.Select(jobRoleId => int.Parse(jobRoleId)));
                return list;
            }

            if (isRequired)
                ModelState.AddModelError(formObjectName, isRequiredErrorMessage);
            return list;
        }

        private void InitializeViewBag(NonTechVolunteerViewModel model)
        {
            ViewBag.Expertise = _expertiseRepository.FindAll().ToSelectList(j => j.Description, j => j.Id.ToString(), j => model != null && model.NonTechJobRoleIds != null && model.NonTechJobRoleIds.Contains(j.Id));
        }
    }
}
