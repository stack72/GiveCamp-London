using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using GiveCampLondon.Repositories;
using GiveCampLondon.Services;
using GiveCampLondon.Website.Helpers;
using GiveCampLondon.Website.Models.Volunteer;

namespace GiveCampLondon.Website.Controllers
{
    public class NonTechVolunteerController : BaseController
    {
        public NonTechVolunteerController(IContentRepository contentRepository,
            INonTechVolunteerRepository volunteerRepository,
            IExpertiseRepository expertiseRepository,
            IMembershipService membershipService,
            ISettingRepository settingRepository)
            : base(settingRepository)
        {
            _membershipService = membershipService;
            _expertiseRepository = expertiseRepository;
            _volunteerRepository = volunteerRepository;
        }

        private readonly INonTechVolunteerRepository _volunteerRepository;
        private readonly IExpertiseRepository _expertiseRepository;
        private readonly IMembershipService _membershipService;

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
            if (User.Identity.IsAuthenticated)
            {
                bool success = UpdateVolunteer(model, selectedExpertiseIds);
                if (success)
                    return RedirectToAction("ThankYou");
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

                return true;
            }
            return false;
        }

        private NonTechVolunteer CreateVolunteer(NonTechVolunteerViewModel model, IEnumerable<int> selectedExpertiseLevels)
        {
            var volunteer = new NonTechVolunteer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                JobDescription = model.JobDescription,
                DietaryNeeds = model.DietaryNeeds,
                TwitterHandle = model.TwitterHandle,
                Bio = model.Bio,
                ShirtSize = model.ShirtSize,
                ShirtStyle = model.ShirtStyle,
                SkillSet = model.SkillsOutline,
                SessionDetails = model.ExpertiseTopic

            };
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

        private bool UpdateVolunteer(NonTechVolunteerViewModel model, IEnumerable<int> selectedExpertiseIds)
        {
            MembershipUser user = _membershipService.GetUserByName(User.Identity.Name);
            var volunteer = _volunteerRepository.Get((Guid)user.ProviderUserKey);
            volunteer.FirstName = model.FirstName;
            volunteer.LastName = model.LastName;
            volunteer.Bio = model.Bio;
            volunteer.DietaryNeeds = model.DietaryNeeds;
            volunteer.Email = model.Email;
            volunteer.JobDescription = model.JobDescription;
            volunteer.PhoneNumber = model.PhoneNumber;
            volunteer.ShirtSize = model.ShirtSize;
            volunteer.ShirtStyle = model.ShirtStyle;
            volunteer.TwitterHandle = model.TwitterHandle;
            volunteer.SkillSet = model.SkillsOutline;
            volunteer.SessionDetails = model.ExpertiseTopic;

            foreach (var expertiseId in selectedExpertiseIds)
            {
                volunteer.AreasOfExpertise.Add(_expertiseRepository.Get(expertiseId));
            }

            if (user.Email != model.Email)
            {
                user.Email = model.Email;
                _membershipService.UpdateUser(user);
            }

            _volunteerRepository.Save(volunteer);
            return true;
        }

    }
}
