using System.Linq;
using System.Web.Mvc;
using GiveCampLondon.Repositories;
using GiveCampLondon.Services;
using GiveCampLondon.Website.Helpers;
using GiveCampLondon.Website.Models;
using GiveCampLondon.Website.Models.Charity;

namespace GiveCampLondon.Website.Controllers
{
    public class CharityController : Controller
    {
        public CharityController(ICharityRepository charityRepository, INotificationService notificationService)
        {
            _charityRepository = charityRepository;
            _notificationService = notificationService;
        }

        private readonly ICharityRepository _charityRepository;
        private readonly INotificationService _notificationService;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {
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
                var charity = vm.MapToCharityModel();
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
    
        public ActionResult SupportedCharities()
        {
            var charities = _charityRepository.GetSupportedCharities();

            var supportedCharities = charities.Select(charity => new SupportedCharityDetails
                                                                     {
                                                                         About = charity.About,
                                                                         CharityName = charity.CharityName,
                                                                         LogoPath = charity.LogoPath,
                                                                         WebSiteUrl = charity.Website
                                                                     }).ToList();
            supportedCharities.Shuffle();

            return PartialView("_SupportedCharitiesPanel", supportedCharities);
        }
    }
}
