using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GiveCampLondon.Repositories;
using GiveCampLondon.Services;
using GiveCampLondon.Website.Models;
using GiveCampLondon.Website.Models.Charity;
using MvcMembership;

namespace GiveCampLondon.Website.Controllers
{
    public class CharityController: BaseController
    {
		public CharityController(IContentRepository contentRepository, ICharityRepository charityRepository, IMembershipService membershipService, IRolesService rolesService, IFormsAuthentication formsAuth, ISettingRepository settingRepository, INotificationService notificationService)
            : base(settingRepository)
        {
            _settingRepository = settingRepository;
		    _rolesService = rolesService;
            _membershipService = membershipService;
		    _charityRepository = charityRepository;
        	_notificationService = notificationService;
        }

        private readonly ICharityRepository _charityRepository;
        private readonly IMembershipService _membershipService;
        private readonly IRolesService _rolesService;
        private readonly ISettingRepository _settingRepository;
		private readonly INotificationService _notificationService;

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Charity"))
            {
                return RedirectToAction("Signup");
            }

            if (User.Identity.IsAuthenticated && User.IsInRole("Administrator"))
            {
                return RedirectToAction("Charities");
            }

        	var settings = _settingRepository.GetSetting(); 

            if(settings != null && settings.PublishCharities)
            {
                return RedirectToAction("ApprovedCharities");
            }
            
            return View();
        }

        public ActionResult SignUp()
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("Charity"))
            {
                var user = _membershipService.GetUserByName(User.Identity.Name);
                var charity =  _charityRepository.Get((Guid) user.ProviderUserKey);
                var model = new SignUpViewModel
                                            {
                                                BackgroundInformation = charity.BackgroundInformation,
                                                Email = user.Email,
                                                Name = charity.CharityName,
                                                OtherInfrastructure = charity.OtherInfrastructure,
                                                OtherSupportSkills = charity.OtherSupportSkills,
                                                WorkRequested = charity.WorkRequested
                                            };
                return View(model);

            }
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpViewModel vm)
        {
            if (!ModelState.IsValid)
                return View();
            bool success = SaveCharity(vm);

            return RedirectToAction(success ? "thankyou" : "SignUp");
        }

        private bool SaveCharity(SignUpViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var charity = new Charity
                        {
                            CharityName = vm.Name,
                            BackgroundInformation = vm.BackgroundInformation,
                            OtherInfrastructure = vm.OtherInfrastructure,
                            OtherSupportSkills = vm.OtherSupportSkills,
                            WorkRequested = vm.WorkRequested,
                            Email = vm.Email,
                            Website = vm.Website,
                            ContactName = vm.ContactName,
                            ContactPhone = vm.ContactPhone,
                            Approved = false
                        };

                _charityRepository.Save(charity);

                _notificationService.SendCharityNotification(charity, CharityNotificationTemplate.WelcomeCharity);

                return true; 
            }
            return false;
        }

        public ActionResult ThankYou()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Charities()
        {
            IEnumerable<CharitySummaryModel> charitySummeries = GetCharitySummeries();
            return View(charitySummeries);
        }


        public ActionResult ApprovedCharities()
        {
            IEnumerable<CharitySummaryModel> charitySummeries = GetCharitySummeries().Where(c => c.Approved);
            return View(charitySummeries);
        }


        private IEnumerable<CharitySummaryModel> GetCharitySummeries()
        {
            return _rolesService.FindUserNamesByRole("Charity")
                .Select(username => _membershipService.GetUserByName(username))
                .Select(membership => _charityRepository.Get((Guid)membership.ProviderUserKey))
                .Select(charity => new CharitySummaryModel{ Email = charity.Email, Name = charity.CharityName, Id = charity.Id, Approved = charity.Approved });
        }

        public ActionResult Details(int id)
        {
            Charity charity = _charityRepository.Get(id);

            return View(charity);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Approve(int id)
        {
            Charity charity = ApproveCharity(id, true);

            return View("Details", charity);
        }


        [Authorize(Roles = "Administrator")]
        public ActionResult Disapprove(int id)
        {
            Charity charity = ApproveCharity(id, false);

            return View("Details", charity);
        }

        
        private Charity ApproveCharity(int id, bool approve)
        {
            Charity charity = _charityRepository.Get(id);
            charity.Approved = approve;
            _charityRepository.Save(charity);
            return charity;
        }
    }
}
