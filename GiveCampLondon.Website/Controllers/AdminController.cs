using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using GiveCampLondon.Repositories;
using GiveCampLondon.Website.Models;
using GiveCampLondon.Website.Models.Admin;

namespace GiveCampLondon.Website.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController: BaseController
    {
        public AdminController(IContentRepository contentRepository,IJobRoleRepository jobRoleRepository,  IVolunteerRepository volunteerRepository, ISettingRepository settingRepository, IDocumentRepository documentRepository)
            : base(settingRepository)
        {
            _jobRoleRepository = jobRoleRepository;
            _volunteerRepository = volunteerRepository;
            _settingRepository = settingRepository;
            _contentRepository = contentRepository;
            _documentRepository = documentRepository;
            _slugs = _contentRepository.GetSlugs();
            _slugSelectList = PopulateSlugDropdown();

        }

        private IContentRepository _contentRepository;
        private IDocumentRepository _documentRepository;
        private static List<string> _slugs;
        private static List<SelectListItem> _slugSelectList;
        private ISettingRepository _settingRepository;
        private IVolunteerRepository _volunteerRepository;
        private IJobRoleRepository _jobRoleRepository;

        public ActionResult ControlPanel()
        {
            var setting = _settingRepository.GetSetting() ?? new Setting();
            var settingModel = new SettingModel
                                   {
                                       City = setting.City,
                                       ContactEmail = setting.ContactEmail,
                                       ContactName = setting.ContactName,
                                       PublishCharities = setting.PublishCharities,
                                       PublishVolunteers = setting.PublishVolunteers,
                                       TwitterTag = setting.TwitterTag
                                   };

            ViewBag.AdminFiles = _documentRepository.GetAllByType("Admin").OrderBy(d => d.Name);

            return View(settingModel);
        }

        [HttpPost]
        public ActionResult ControlPanel(SettingModel settingModel)
        {
            var setting = _settingRepository.GetSetting() ?? new Setting();

            setting.City = settingModel.City;
            setting.ContactEmail = settingModel.ContactEmail;
            setting.ContactName = settingModel.ContactName;
            setting.PublishCharities = settingModel.PublishCharities;
            setting.PublishVolunteers = settingModel.PublishVolunteers;
            setting.TwitterTag = settingModel.TwitterTag;
   
            _settingRepository.SaveSetting(setting);

            ViewBag.AdminFiles = _documentRepository.GetAllByType("Admin").OrderBy(d => d.Name);
            return View(settingModel);
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
            StringBuilder tagList =new StringBuilder();

            if(!string.IsNullOrEmpty(slug))
            {
                _contentRepository.GetTags(slug).ForEach(t => tagList.Append(string.Format("<option value='{0}'>{0}</option>", t)));
            }

            return base.Content(tagList.ToString());
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
            _jobRoleRepository.Save(new JobRole{Description = name, DisplayOrder = 0});
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

            if(!string.IsNullOrEmpty(slug) & !string.IsNullOrEmpty(tag))
            {
                content = _contentRepository.Get(slug, tag);
            }

            return Json(content, JsonRequestBehavior.AllowGet);
        }


        public ActionResult File(int documentId = 0, string option = null)
        {
            var document = _documentRepository.Get(documentId);
            if (option == "delete" & document != null)
            {
                _documentRepository.Delete(document);
                document = new Document();
            }

            ViewBag.DocumentList = _documentRepository.GetAll().OrderBy(d => d.Type).OrderBy(d => d.Name);
            ViewBag.DocumentTypeList = PopulateDocumentTypeDropdown();

            return View(document ?? new Document());
        }

        [HttpPost]
        public ActionResult File(Document document)
        {
            if(document.DocumentId!=0)
            {

                var dbDocument = _documentRepository.Get(document.DocumentId);
                document.LocalFilename = dbDocument.LocalFilename;
                document.MimeType = dbDocument.MimeType;
                document.UploadDate = dbDocument.UploadDate;
            }

            var httpFile = Request.Files["UploadFile"];
            if (httpFile != null)
            {
                document.MimeType =httpFile.ContentType;
                document.OriginalFilename = Path.GetFileName(httpFile.FileName);
                document.UploadDate = DateTime.Now;

                _documentRepository.Save(document,httpFile.InputStream);
            }
            else
            {
                _documentRepository.Save(document);
            }

            ViewBag.DocumentList = _documentRepository.GetAll().OrderBy(d => d.Type).OrderBy(d => d.Name);
            ViewBag.DocumentTypeList = PopulateDocumentTypeDropdown();

            return View(document);
        }


        private List<SelectListItem> PopulateSlugDropdown()
        {
            var selectList = new List<SelectListItem>();
            _slugs.ForEach(s => selectList.Add(new SelectListItem() { Text = s, Value = s }));
                                           
            return selectList;
        }


        private List<SelectListItem> PopulateDocumentTypeDropdown()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem() {Text="Volunteer", Value="Volunteer"});
            selectList.Add(new SelectListItem() { Text = "Charity", Value = "Charity" });
            selectList.Add(new SelectListItem() { Text = "Admin", Value = "Admin" });
            selectList.Add(new SelectListItem() { Text = "Hold", Value = "Hold" });

            return selectList;
        }
    }
}
