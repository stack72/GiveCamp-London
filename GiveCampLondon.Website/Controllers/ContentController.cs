using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiveCampLondon.Repositories;
using GiveCampLondon.Website.Helpers;

namespace GiveCampLondon.Website.Controllers
{
    public class ContentController : Controller
    {
        private readonly ISponsorRepository _sponsorRepository;
        private readonly IWaitListHelper _waitListHelper;
        public ContentController(ISponsorRepository sponsorsRepository, IWaitListHelper waitListHelper)
        {
            _sponsorRepository = sponsorsRepository;
            _waitListHelper = waitListHelper;
        }

        //
        // GET: /Content/
        [ChildActionOnly]
        public ActionResult RotatorContent()
        {
            var sponsors = GetSponsorsFromCache();
            if (sponsors == null)
            {
                sponsors = _sponsorRepository.FindAll();
                HttpRuntime.Cache.Insert("LeftHandPanelSponsors", sponsors);
            }
            
            sponsors.Shuffle();
            return PartialView(sponsors);
        }

        [ChildActionOnly]
        public ActionResult EventFullBanner()
        {
            ViewBag.IsEventFull = _waitListHelper.SetWaitListStatus();
            return PartialView();
        }

        private IList<Sponsor> GetSponsorsFromCache()
        {
            if (HttpRuntime.Cache["LeftHandPanelSponsors"] != null)
            {
                return HttpRuntime.Cache["LeftHandPanelSponsors"] as IList<Sponsor>;
            }

            return null;
        }
    }
}
