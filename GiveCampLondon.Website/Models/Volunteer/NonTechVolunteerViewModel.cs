using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GiveCampLondon.Website.Models.Volunteer
{
    public class NonTechVolunteerViewModel
    {
        public NonTechVolunteerViewModel()
        {
            NonTechJobRoleIds = new List<int>();
            Availability = new List<int>();
        }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address:")]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        [DisplayName("First Name:")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Text)]
        [DisplayName("Last Name:")]
        public string LastName { get; set; }

        [StringLength(50)]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone:")]
        public string PhoneNumber { get; set; }

        [DisplayName("I am primarily a:")]
        public IList<int> NonTechJobRoleIds { get; set; }

        [StringLength(100)]
        [DataType(DataType.Text)]
        [DisplayName("Your day job (e.g. Community Evangelist for DevExpress):")]
        public string JobDescription { get; set; }

        [DisplayName("Shirt Size:")]
        public string ShirtSize { get; set; }

        [DisplayName("Shirt Style:")]
        public string ShirtStyle { get; set; }

        [StringLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("Dietary Needs:")]
        public string DietaryNeeds { get; set; }

        [StringLength(50)]
        [DataType(DataType.Text)]
        [DisplayName("Twitter Handle:")]
        public string TwitterHandle { get; set; }

        [StringLength(4000)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Brief Bio:")]
        public string Bio { get; set; }

        [StringLength(4000)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Outline your skills:")]
        public string SkillsOutline { get; set; }

        [StringLength(4000)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Expertise:")]
        public string ExpertiseTopic { get; set; }

        [DisplayName("I am primarily a:")]
        public IList<int> Availability { get; set; }
    }
}
