﻿using System;
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
            IExperienceLevelRepository xpLevelRepository, IRolesService rolesService, IMembershipService membershipService, 
            ICharityRepository charityRepository, ISponsorRepository sponsorRepository)
        {
            _jobRoleRepository = jobRoleRepository;
            _volunteerRepository = volunteerRepository;
            _nonTechieVolunteerRepository = nonTechieVolunteerRepository;
            _contentRepository = contentRepository;
            _xpLevelRepository = xpLevelRepository;
            _rolesService = rolesService;
            _membershipService = membershipService;
            _charityRepository = charityRepository;
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
        private readonly IRolesService _rolesService;
        private readonly IMembershipService _membershipService;
        private readonly ICharityRepository _charityRepository;
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
            IEnumerable<VolunteerSummaryModel> volunteerSummaries = _volunteerRepository.FindAll()
                .Select(volunteer => new VolunteerSummaryModel
                {
                    Id = volunteer.Id,
                    LastName = volunteer.LastName,
                    FirstName = volunteer.FirstName,
                    Email = volunteer.Email,
                    PhoneNumber = volunteer.PhoneNumber,
                    TeamName = volunteer.TeamName,
                    TwitterHandle = volunteer.TwitterHandle
                });
            return View(volunteerSummaries);
        }

        public ActionResult NonTechies()
        {
            IEnumerable<NonTechieVolunteerSummaryModel> nonTechieVolunteers = _nonTechieVolunteerRepository.FindAll()
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

        public ActionResult Charities()
        {
            IEnumerable<CharitySummaryModel> charitySummeries = GetCharitySummeries();
            return View(charitySummeries);
        }

        public ActionResult Approve(int id)
        {
            Charity charity = ApproveCharity(id, true);
            return View("CharityDetails", charity);
        }

        public ActionResult Disapprove(int id)
        {
            Charity charity = ApproveCharity(id, false);
            return View("CharityDetails", charity);
        }

        public ActionResult CharityDetails(int id)
        {
            Charity charity = _charityRepository.Get(id);
            return View(charity);
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
            var users = _volunteerRepository.FindAll().Select(x => x.Email).Distinct();
            var notTechies = _nonTechieVolunteerRepository.FindAll().Select(x => x.Email).Distinct();

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
            var name = sponsorName.Replace(" ", "_");
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

        private Charity ApproveCharity(int id, bool approve)
        {
            Charity charity = _charityRepository.Get(id);
            charity.Approved = approve;
            _charityRepository.Save(charity);
            return charity;
        }

        private static List<SelectListItem> PopulateSlugDropdown()
        {
            var selectList = new List<SelectListItem>();
            _slugs.ForEach(s => selectList.Add(new SelectListItem { Text = s, Value = s }));

            return selectList;
        }

        private IEnumerable<CharitySummaryModel> GetCharitySummeries()
        {
            return _rolesService.FindUserNamesByRole("Charity")
                .Select(username => _membershipService.GetUserByName(username))
                .Select(membership => _charityRepository.Get((Guid)membership.ProviderUserKey))
                .Select(charity => new CharitySummaryModel { Email = charity.Email, Name = charity.CharityName, Id = charity.Id, Approved = charity.Approved });
        }
    }
}
