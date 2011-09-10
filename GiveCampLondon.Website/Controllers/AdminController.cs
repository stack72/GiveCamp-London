using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using GiveCampLondon.Repositories;
using GiveCampLondon.Website.Models;
using GiveCampLondon.Website.Models.Volunteer;

namespace GiveCampLondon.Website.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        public AdminController(IContentRepository contentRepository, IJobRoleRepository jobRoleRepository,
         INonTechVolunteerRepository nonTechieVolunteerRepository)
        {
            _jobRoleRepository = jobRoleRepository;
            _nonTechieVolunteerRepository = nonTechieVolunteerRepository;
            _contentRepository = contentRepository;
            _slugs = _contentRepository.GetSlugs();
            _slugSelectList = PopulateSlugDropdown();
        }

        private readonly IContentRepository _contentRepository;
        private static List<string> _slugs;
        private static List<SelectListItem> _slugSelectList;
        private readonly INonTechVolunteerRepository _nonTechieVolunteerRepository;
        private readonly IJobRoleRepository _jobRoleRepository;

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

        public JsonResult GetContent(string slug, string tag)
        {
            Content content = null;

            if (!string.IsNullOrEmpty(slug) & !string.IsNullOrEmpty(tag))
            {
                content = _contentRepository.Get(slug, tag);
            }

            return Json(content, JsonRequestBehavior.AllowGet);
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


        public ActionResult NonTechieDetails(int id)
        {
            var nonTechie = _nonTechieVolunteerRepository.Get(id);
            nonTechie.AreasOfExpertise = _nonTechieVolunteerRepository.FindExpertiseFor(nonTechie.Id);

            return View(nonTechie);
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

        private static List<SelectListItem> PopulateSlugDropdown()
        {
            var selectList = new List<SelectListItem>();
            _slugs.ForEach(s => selectList.Add(new SelectListItem { Text = s, Value = s }));

            return selectList;
        }
}
}
