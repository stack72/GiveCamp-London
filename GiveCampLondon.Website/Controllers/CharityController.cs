using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using GiveCampLondon.Repositories;
using GiveCampLondon.Services;
using GiveCampLondon.Website.Helpers;
using GiveCampLondon.Website.Models;
using GiveCampLondon.Website.Models.Charity;
using MvcMembership;
using PagedList;


namespace GiveCampLondon.Website.Controllers
{
    public class CharityController: BaseController
    {
		public CharityController(IContentRepository contentRepository, ICharityRepository charityRepository, IMembershipService membershipService, IRolesService rolesService, IFormsAuthentication formsAuth, ISettingRepository settingRepository, INotificationService notificationService)
            :base(settingRepository)
        {
            _settingRepository = settingRepository;
            _formsAuth = formsAuth;
            _rolesService = rolesService;
            _membershipService = membershipService;
            _contentRepository = contentRepository;
            _charityRepository = charityRepository;
        	_notificationService = notificationService;
        }

        private IContentRepository _contentRepository;
        private ICharityRepository _charityRepository;
        private IMembershipService _membershipService;
        private IRolesService _rolesService;
        private IFormsAuthentication _formsAuth;
        private ISettingRepository _settingRepository;
		private INotificationService _notificationService;

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
            var content = _contentRepository.Get("charity-index", "blurb");
            if (content != null)
                ViewBag.Content = content;

            return View();
        }

        public ActionResult SignUp()
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("Charity"))
            {
                MembershipUser user = _membershipService.GetUserByName(User.Identity.Name);
                Charity charity =  _charityRepository.Get((Guid) user.ProviderUserKey);
                string fakePassword = Guid.NewGuid().ToString();
                SignUpViewModel model = new SignUpViewModel()
                                            {
                                                BackgroundInformation = charity.BackgroundInformation,
                                                Email = user.Email,
                                                Name = charity.Name,
                                                UserName = user.UserName,
                                                OtherInfrastructure = charity.OtherInfrastructure,
                                                OtherSupportSkills = charity.OtherSupportSkills,
                                                WorkRequested = charity.WorkRequested,
                                                Password = fakePassword,
                                                ConfirmPassword = fakePassword

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
            bool success = User.Identity.IsAuthenticated && User.IsInRole("Charity") ? UpdateCharity(vm) : SaveCharity(vm);

            return RedirectToAction(success ? "thankyou" : "SignUp");
        }

        private bool SaveCharity(SignUpViewModel vm)
        {
            MembershipCreateStatus signupStatus = _membershipService.CreateUser(vm.UserName, vm.Password, vm.Email);
            if (signupStatus == MembershipCreateStatus.Success)
            {
                MembershipUser user = _membershipService.GetUserByName(vm.UserName);
                _rolesService.AddToRole(user, "Charity");
                var charity = new Charity
                {
                    Name = vm.Name,
                    MembershipId = (Guid)user.ProviderUserKey,
                    BackgroundInformation = vm.BackgroundInformation,
                    OtherInfrastructure = vm.OtherInfrastructure,
                    OtherSupportSkills = vm.OtherSupportSkills,
                    WorkRequested = vm.WorkRequested,
                    Email = vm.Email,
                    Approved = false
                };

                _charityRepository.Save(charity);

            	_notificationService.SendCharityNotification(charity, CharityNotificationTemplate.WelcomeCharity);

				_formsAuth.SignIn(vm.UserName, false);

                return true;
            }
            return false;
        }

        private bool UpdateCharity(SignUpViewModel vm)
        {
            MembershipUser user = _membershipService.GetUserByName(User.Identity.Name);
            Charity charity = _charityRepository.Get((Guid)user.ProviderUserKey);

            charity.Name = vm.Name;
            charity.BackgroundInformation = vm.BackgroundInformation;
            charity.OtherInfrastructure = vm.OtherInfrastructure;
            charity.OtherSupportSkills = vm.OtherSupportSkills;
            charity.WorkRequested = vm.WorkRequested;
            charity.Email = vm.Email;

            if(user.Email != vm.Email)
            {
                user.Email = vm.Email;
                _membershipService.UpdateUser(user);
            }

            _charityRepository.Save(charity);
            return true;
        }

        public ActionResult ThankYou()
        {
            ViewBag.Content = _contentRepository.Get("charity-thankyou", "blurb");

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
                .Select(charity => new CharitySummaryModel{ Email = charity.Email, Name = charity.Name, Id = charity.Id, Approved = charity.Approved });
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
