using System;
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
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(4000)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Background Information")]
        public string BackgroundInformation { get; set; }

        [Required]
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
    }
}