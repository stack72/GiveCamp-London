using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiveCampLondon.Repositories;

namespace GiveCampLondon.Website.Controllers
{
    public class DocumentController : BaseController
    {



        public DocumentController(IDocumentRepository documentRepository, ISettingRepository settingRepository)
            : base(settingRepository)
        {
            _settingRepository = settingRepository;
            _documentRepository = documentRepository;
        }

        private IDocumentRepository _documentRepository;
        private ISettingRepository _settingRepository;


        // NOTE: There should probably besome security here.
        [HttpGet]
        public ActionResult GetDocument(int documentId)
        {
            var document = _documentRepository.Get(documentId);
            return base.File(_documentRepository.GetFile(document), document.MimeType);
        }

    }
}
