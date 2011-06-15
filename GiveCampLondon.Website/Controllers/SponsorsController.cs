using System.Web.Mvc;
using GiveCampLondon.Repositories;
using GiveCampLondon.Website.Helpers;

namespace GiveCampLondon.Website.Controllers
{
    public class SponsorsController : Controller
    {
        private readonly ISponsorRepository _sponsorRepository;

        public SponsorsController(ISponsorRepository sponsorRepository)
        {
            _sponsorRepository = sponsorRepository;
        }

        //
        // GET: /Sponsors/

        public ActionResult Index()
        {
            var sponsors = _sponsorRepository.FindAll();
            sponsors.Shuffle();

            return View(sponsors);
        }

        

    }
}
