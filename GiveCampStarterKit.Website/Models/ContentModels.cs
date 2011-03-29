using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GiveCampStarterKit.Website.Models
{
    #region models

    public class EditContentViewModel
    {

        public string Slug { get; set; }
        public string Tag { get; set; }

        public List<SelectListItem> SlugSelectList { get; set; }
        public List<SelectListItem> TagSelectList { get; set; }

        [StringLength(500)]
        [AllowHtml]
        public string Title { get; set; }
                
        [AllowHtml]
        public string ContentText { get; set; }
    }

    #endregion
}