using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using GiveCampLondon.Repositories;

namespace GiveCampLondon.Website.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SponsorsAdminController : Controller
    {
        private readonly ISponsorRepository _sponsorRepository;
     
        public SponsorsAdminController(ISponsorRepository sponsorRepository)
        {
            _sponsorRepository = sponsorRepository;
        }

        public ActionResult Sponsors()
        {
            IEnumerable<Sponsor> sponsors = _sponsorRepository.FindAll();
            return View(sponsors);
        }

        public ActionResult AddSponsor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSponsor(Sponsor sponsor, HttpPostedFileBase mainLogo, HttpPostedFileBase smallLogo)
        {
            if (ModelState.IsValid)
            {
                TrySaveImages(mainLogo, smallLogo, sponsor.Name);
                var sponsorToSave = new Sponsor
                {
                    Blurb = sponsor.Blurb,
                    Link = "http://" + sponsor.Link,
                    Name = sponsor.Name,
                    MainLogo = FormatLogoName(mainLogo, sponsor.Name, "mainlogo"),
                    SmallLogo = FormatLogoName(smallLogo, sponsor.Name, "smalllogo")
                };
                _sponsorRepository.Save(sponsorToSave);

                return RedirectToAction("Sponsors");
            }
            else
                return View(sponsor);
        }

        private string FormatLogoName(HttpPostedFileBase logo, string sponsorName, string logoType)
        {
            return string.Format("{0}_{1}{2}", logoType, sponsorName, Path.GetExtension(logo.FileName));
        }

        private void TrySaveImages(HttpPostedFileBase mainLogo, HttpPostedFileBase smallLogo, string sponsorName)
        {
            SaveLogo(mainLogo, "mainlogo", sponsorName);
            SaveLogo(smallLogo, "smalllogo", sponsorName);
        }

        private void SaveLogo(HttpPostedFileBase logo, string logoType, string sponsorName)
        {
            if (logo != null && logo.ContentLength > 0)
            {
                var logoRepoPath = string.Format("~/Content/images/sponsors/{0}", logoType);

                var fileName = FormatLogoName(logo, sponsorName, logoType);
                var path = Path.Combine(Server.MapPath(logoRepoPath), fileName);
                logo.SaveAs(path);
            }
        }

    }
}
