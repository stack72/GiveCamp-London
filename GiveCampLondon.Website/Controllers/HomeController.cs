using System;
using System.Web.Mvc;
using GiveCampLondon.Services;
using GiveCampLondon.Website.Models;

namespace GiveCampLondon.Website.Controllers
{
    public class HomeController : Controller
    {
        private INotificationService _notificationService;
        public HomeController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Schedule()
        {
            return View();
        }

        public ActionResult Location()
        {
            return View();
        }
    
        [ActionName("Contact-Us")]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Contact-Us")]
        public ActionResult ContactUs(ContactUsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _notificationService.SendContactUsMail(vm.Enquiry, vm.EmailAddress);
                    ViewBag.RequestSent = true;
                }
                catch (Exception)
                {
                    ViewBag.RequestSent = false;
                    return View(vm);
                }
            }
            return View(vm);
        }
    }
}
