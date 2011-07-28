using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using GiveCampLondon.Repositories;
using GiveCampLondon.Website.Helpers;
using GiveCampLondon.Website.Models;

namespace GiveCampLondon.Website.Controllers
{
    public class SponsorsController : Controller
    {
        private readonly ISponsorRepository _sponsorRepository;

        public SponsorsController(ISponsorRepository sponsorRepository)
        {
            _sponsorRepository = sponsorRepository;
        }

        public ActionResult Index()
        {
            var sponsors = _sponsorRepository.FindAll();
            
            var viewModel = BuildSponsorsViewModel(sponsors);
            viewModel.Contributors.Shuffle();
            viewModel.Sponsors.Shuffle();

            return View(viewModel);
        }

        private SponsorsViewModel BuildSponsorsViewModel(IList<Sponsor> sponsors)
        {
            var viewModel = new SponsorsViewModel
                                {
                                    Sponsors = (from sponsorList in sponsors
                                                where sponsorList.IsContributor
                                                select sponsorList).ToList(),
                                    Contributors = (from sponsorList in sponsors
                                                    where sponsorList.IsContributor == false
                                                    select sponsorList).ToList()
                                };
            return viewModel;
        }
    }
}
