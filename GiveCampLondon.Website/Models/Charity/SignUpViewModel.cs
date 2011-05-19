using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GiveCampLondon.Website.Models.Charity
{
    public class SignUpViewModel
    {
        [Required]
        [StringLength(100)]
        [DisplayName("Charity Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [StringLength(4000)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Background Information")]
        public string BackgroundInformation { get; set; }

        [StringLength(4000)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Work Requested")]
        public string WorkRequested { get; set; }

        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Other Infrastructure")]
        public string OtherInfrastructure { get; set; }

        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Other Support Skills")]
        public string OtherSupportSkills { get; set; }

        [StringLength(1000)]
        [DisplayName("Website")]
        public string Website { get; set; }

        [StringLength(1000)]
        [DisplayName("Contact Name")]
        public string ContactName { get; set; }

        [StringLength(1000)]
        [DisplayName("Contact Phone")]
        public string ContactPhone { get; set; }
    }
}