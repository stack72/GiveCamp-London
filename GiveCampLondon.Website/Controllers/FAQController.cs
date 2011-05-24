using System.Web.Mvc;

namespace GiveCampLondon.Website.Controllers
{
    public class FAQController : Controller
    {
        public ActionResult FAQ(string id)
        {
            // just FAQs with no section specified
            if (string.IsNullOrEmpty(id))
            {
                return View();
            }

            // section specified
            switch (id.ToLower())
            {
                case "charities":
                    return (View("FAQ-Charities"));
                case "developers":
                    return (View("FAQ-Developers"));
                case "eventstaff":
                    return (View("FAQ-EventStaff"));
                case "sponsors":
                    return (View("FAQ-Sponsors"));
                default:
                    // someone typed in a non-existant section URL
                    // redirect them to the 'no section specified' case
                    return RedirectToAction("FAQ");
            }
        }
    }
}
