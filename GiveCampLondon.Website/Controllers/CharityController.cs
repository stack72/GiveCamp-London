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
    public class CharityController : Controller
    {
        public CharityController(IContentRepository contentRepository, ICharityRepository charityRepository, 
            IMembershipService membershipService, IFormsAuthentication formsAuth,
            INotificationService notificationService)
        {
            _membershipService = membershipService;
            _charityRepository = charityRepository;
            _notificationService = notificationService;
        }

        private readonly ICharityRepository _charityRepository;
        private readonly IMembershipService _membershipService;
        private readonly INotificationService _notificationService;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Charity"))
            {
                var user = _membershipService.GetUserByName(User.Identity.Name);
                var charity = _charityRepository.Get((Guid)user.ProviderUserKey);
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
                _notificationService.SendNotification(vm.Email, VolunteerNotificationTemplate.WelcomeVolunteer);

                return true;
            }
            return false;
        }

        public ActionResult ThankYou()
        {
            return View();
        }
    }
}
