using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GiveCampLondon.Repositories;
using GiveCampLondon.Services;
using GiveCampLondon.Website.Helpers;
using GiveCampLondon.Website.Models;
using GiveCampLondon.Website.Models.Volunteer;

namespace GiveCampLondon.Website.Controllers
{
    public class VolunteerController: Controller
    {
        public VolunteerController(IWaitListHelper waitListHelper, 
            IVolunteerRepository volunteerRepository,
			IJobRoleRepository jobRoleRepository, 
			INotificationService notificationService, 
            ITechnologyRepository technologyRepository, 
            IExperienceLevelRepository xpLevelRepository)
        {
            _waitListHelper = waitListHelper;
            _xpLevelRepository = xpLevelRepository;
            _technologyRepository = technologyRepository;
            _jobRoleRepository = jobRoleRepository;
            _volunteerRepository = volunteerRepository;
        	_notificationService = notificationService;
        }

        private readonly IWaitListHelper _waitListHelper;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IJobRoleRepository _jobRoleRepository;
        private readonly ITechnologyRepository _technologyRepository;
    	private readonly INotificationService _notificationService;
        private readonly IExperienceLevelRepository _xpLevelRepository;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            InitializeViewBag(null);
            return View();
        }
        
        [HttpPost]
        public ActionResult SignUp(SignUpViewModel model, FormCollection collection)
        {
            var selectedJobRoleIds = GetSelectedValues(collection, "jobRoles", true, "Please select at least one job role.");
            var selectedTechnologyIds = GetSelectedValues(collection, "technologies", true, "Please select at least one technology");

            if (!ModelState.IsValid)
            {
                InitializeViewBag(model);
                return View();
            }
            
            if (SaveVolunteer(model, selectedJobRoleIds, selectedTechnologyIds))
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

        private bool SaveVolunteer(SignUpViewModel model, IEnumerable<int> selectedJobRoleIds, IEnumerable<int> selectedTechnologyIds)
        {
            if (ModelState.IsValid)
            {
                var volunteer = CreateVolunteer(model, selectedJobRoleIds, selectedTechnologyIds);
                
                _volunteerRepository.Save(volunteer);
                _notificationService.SendNotification(model.Email,
                                                      volunteer.IsOnWaitList
                                                          ? VolunteerNotificationTemplate.WelcomeWaitingVolunteer
                                                          : VolunteerNotificationTemplate.WelcomeVolunteer);


                return true;
            }
            return false;
        }

        private Volunteer CreateVolunteer(SignUpViewModel model, IEnumerable<int> selectedJobRoleIds, IEnumerable<int> selectedTechnologyIds)
        {
            var volunteer = model.MapToVolunteerModel();
            volunteer.ExperienceLevel = _xpLevelRepository.Get(model.ExperienceLevel);
            volunteer.IsOnWaitList = _waitListHelper.SetWaitListStatus();        
            foreach (var jobRoleId in selectedJobRoleIds)
            {
                volunteer.JobRoles.Add(_jobRoleRepository.Get(jobRoleId));
            }

            foreach (var selectedTechnologyId in selectedTechnologyIds)
            {
                volunteer.Technologies.Add(_technologyRepository.Get(selectedTechnologyId));
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

        private void InitializeViewBag(SignUpViewModel model)
        {
            ViewBag.JobRoles = _jobRoleRepository.FindAll().ToSelectList(j => j.Description, j => j.Id.ToString(), j => model != null && model.JobRoleIds != null && model.JobRoleIds.Contains(j.Id));
            ViewBag.Technologies = _technologyRepository.FindAll().ToSelectList(t => t.Description, t => t.Id.ToString(), t => model != null && model.TechnologyIds != null && model.TechnologyIds.Contains(t.Id));
            ViewBag.ExperienceLevels = _xpLevelRepository.FindAll().ToSelectList(e => e.Description, e => e.Id.ToString(), e => model != null && model.ExperienceLevel == e.Id);
        }
    }
}
