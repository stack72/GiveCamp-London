using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GiveCampLondon.Website.Models.Admin
{
    public class SettingModel
    {
        [Required]
        [StringLength(50)]
        [DisplayName("Event City")]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Contact Name")]
        public string ContactName { get; set; }


        [Required]
        [StringLength(50)]
        [DisplayName("Contact Email")]
        public string ContactEmail { get; set; }


        [DisplayName("Twitter Tag")]
        public string TwitterTag { get; set; }

         [DisplayName("Publish Charities (Freeze Charity Signup)")]
        public bool PublishCharities { get; set; }

        [DisplayName("Publish Volunteers (Freeze Volunteer Signup)")]
        public bool PublishVolunteers { get; set; }
    }
}