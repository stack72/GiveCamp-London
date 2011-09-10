using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using GiveCampLondon.Repositories;
using GiveCampLondon.Services;
using GiveCampLondon.Website.Models;
using GiveCampLondon.Website.Models.Charity;
using GiveCampLondon.Website.Models.Volunteer;
using MvcMembership;

namespace GiveCampLondon.Website.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        public AdminController(IContentRepository contentRepository, IJobRoleRepository jobRoleRepository,
            IVolunteerRepository volunteerRepository, INonTechVolunteerRepository nonTechieVolunteerRepository,
            IExperienceLevelRepository xpLevelRepository, ISponsorRepository sponsorRepository)
        {
            _jobRoleRepository = jobRoleRepository;
            _volunteerRepository = volunteerRepository;
            _nonTechieVolunteerRepository = nonTechieVolunteerRepository;
            _contentRepository = contentRepository;
            _xpLevelRepository = xpLevelRepository;
            _sponsorRepository = sponsorRepository;
            _slugs = _contentRepository.GetSlugs();
            _slugSelectList = PopulateSlugDropdown();
        }

        private readonly IExperienceLevelRepository _xpLevelRepository;
        private readonly IContentRepository _contentRepository;
        private static List<string> _slugs;
        private static List<SelectListItem> _slugSelectList;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly INonTechVolunteerRepository _nonTechieVolunteerRepository;
        private readonly IJobRoleRepository _jobRoleRepository;

        private readonly ISponsorRepository _sponsorRepository;

        public ActionResult ControlPanel()
        {
            return View();
        }

        public ActionResult EditContent()
        {
            ViewBag.SlugSelectList = _slugSelectList;
            ViewBag.TagSelectList = new List<SelectListItem>();

            return View();
        }

        [HttpPost]
        public ActionResult EditContent(EditContentViewModel contentViewModel)
        {

            if (contentViewModel != null)
            {
                if (ModelState.IsValid)
                {
                    var content = _contentRepository.Get(contentViewModel.Slug, contentViewModel.Tag);
                    content.Title = contentViewModel.Title;
                    content.ContentText = contentViewModel.ContentText;
                    content.PostDate = DateTime.Now;


                    _contentRepository.Save(content);
                }
            }

            ViewBag.SlugSelectList = _slugSelectList;
            ViewBag.TagSelectList = new List<SelectListItem>();

            return View(contentViewModel);
        }

        public ActionResult GetTags(string slug)
        {
            var tagList = new StringBuilder();

            if (!string.IsNullOrEmpty(slug))
            {
                _contentRepository.GetTags(slug).ForEach(t => tagList.Append(string.Format("<option value='{0}'>{0}</option>", t)));
            }

            return Content(tagList.ToString());
        }

        public ActionResult JobRoles()
        {
            return View(_jobRoleRepository.FindAll());
        }

        public ActionResult JobRole(int id)
        {
            return View(_volunteerRepository.FindVolunteersForJobRole(id));
        }

        public ActionResult AddRole(string name)
        {
            _jobRoleRepository.Save(new JobRole { Description = name, DisplayOrder = 0 });
            return RedirectToAction("JobRoles");
        }

        public ActionResult DeleteRole(int id)
        {
            var role = _jobRoleRepository.Get(id);
            _volunteerRepository.RemoveAllVolunteersFromJobRole(id);
            _jobRoleRepository.Delete(role);
            return RedirectToAction("JobRoles");
        }

        public JsonResult GetContent(string slug, string tag)
        {
            Content content = null;

            if (!string.IsNullOrEmpty(slug) & !string.IsNullOrEmpty(tag))
            {
                content = _contentRepository.Get(slug, tag);
            }

            return Json(content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Techies()
        {
            var volunteers = _volunteerRepository.FindAll();
            var techies = BuildTechiesViewModel(volunteers);

            return View(techies);
        }
        
        public ActionResult NonTechies()
        {
            IEnumerable<NonTechieVolunteerSummaryModel> nonTechieVolunteers = _nonTechieVolunteerRepository.FindAll()
                .Where(x => x.HasCancelled == false)
                .Select(volunteer => new NonTechieVolunteerSummaryModel
                {
                    Id = volunteer.Id,
                    LastName = volunteer.LastName,
                    FirstName = volunteer.FirstName,
                    Email = volunteer.Email,
                    PhoneNumber = volunteer.PhoneNumber,
                    TwitterHandle = volunteer.TwitterHandle
                });
            return View(nonTechieVolunteers);
        }

        public ActionResult TechieDetails(int id)
        {
            var volunteer = _volunteerRepository.Get(id);

            volunteer.JobRoles = _volunteerRepository.FindJobRolesFor(volunteer.Id);
            volunteer.Technologies = _volunteerRepository.FindTechnologiesFor(volunteer.Id);
            volunteer.ExperienceLevel = _xpLevelRepository.GetForVolunteerId(volunteer.Id);
            return View(volunteer);
        }

        public ActionResult NonTechieDetails(int id)
        {
            var nonTechie = _nonTechieVolunteerRepository.Get(id);
            nonTechie.AreasOfExpertise = _nonTechieVolunteerRepository.FindExpertiseFor(nonTechie.Id);

            return View(nonTechie);
        }

        [HttpPost]
        public ActionResult TechieCancellation(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _volunteerRepository.CancelRegistration(Id);
                    return RedirectToAction("Techies");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error trying to deregister user", ex);
                    return View("Techies");
                }
            }
            return View("Techies");
        }

        [HttpPost]
        public ActionResult NonTechieCancellation(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _nonTechieVolunteerRepository.CancelRegistration(Id);
                    return RedirectToAction("NonTechies");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error trying to deregister user", ex);
                    return View("NonTechies");
                }
            }
            return View("NonTechies");
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

                return RedirectToAction("Sponsors", "admin");
            }
            else
                return View(sponsor);
        }

        public FileContentResult DownloadEmailList()
        {
            var users = _volunteerRepository.FindAll().Where(x => x.HasCancelled == false).Select(x => x.Email).Distinct();
            var notTechies = _nonTechieVolunteerRepository.FindAll().Where(x => x.HasCancelled == false).Select(x => x.Email).Distinct();

            var sb = new StringBuilder();
            foreach (var user in users)
            {
                sb.AppendFormat(user + ",");
            }

            foreach (var notTechy in notTechies)
            {
                sb.AppendFormat(notTechy + ",");
            }

            return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", "UserMailAddress.csv");
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

        private static List<SelectListItem> PopulateSlugDropdown()
        {
            var selectList = new List<SelectListItem>();
            _slugs.ForEach(s => selectList.Add(new SelectListItem { Text = s, Value = s }));

            return selectList;
        }

        private static TechieSummary BuildTechiesViewModel(IList<Volunteer> volunteers)
        {
            var techies = new TechieSummary
            {
                Volunteers = from volunteer in volunteers
                                 .Where(x => x.HasCancelled == false)
                             select new VolunteerSummaryModel
                             {
                                 Email = volunteer.Email,
                                 FirstName = volunteer.FirstName,
                                 Id = volunteer.Id,
                                 LastName = volunteer.LastName,
                                 PhoneNumber = volunteer.PhoneNumber,
                                 TeamName = volunteer.TeamName,
                                 TwitterHandle = volunteer.TwitterHandle
                             },
                TotalSignups = volunteers.Count,
                OnWaitListVolunteers = (from count in volunteers
                                         .Where(x => x.IsOnWaitList)
                                         .Where(x => x.HasCancelled == false)
                                     select count).Count()
            };
            techies.RegisteredTechies = techies.TotalSignups - techies.OnWaitListVolunteers;
            return techies;
        }
    }
}
