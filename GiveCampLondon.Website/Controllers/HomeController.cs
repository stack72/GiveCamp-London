using System;
using System.Web.Mvc;
using GiveCampLondon.Website.Models;

namespace GiveCampLondon.Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult FAQ(string id)
        {
            // just FAQs with no section specified
            if (string.IsNullOrEmpty(id))
            {
                return View();
            }

            // section specified
            switch (id)
            {
                case "Charities":
                    return (View("FAQ-Charities"));
                case "Developers":
                    return (View("FAQ-Developers"));
                case "EventStaff":
                    return (View("FAQ-EventStaff"));
                case "Sponsors":
                    return (View("FAQ-Sponsors"));
                default:
                    // someone typed in a non-existant section URL
                    // redirect them to the 'no section specified' case
                    return RedirectToAction("FAQ", "Home");
            }
        }

        public ActionResult Agenda()
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
            ContactUsViewModel model;
            try
            {
                //do something here to mail the details
                ViewBag.RequestSent = true;
                model = new ContactUsViewModel();
            }
            catch (Exception)
            {
                model = vm;
            }

            return View(model);
        }
    }
}
