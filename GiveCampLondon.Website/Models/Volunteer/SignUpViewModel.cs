using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GiveCampLondon.Website.Models.Volunteer
{
    public class SignUpViewModel
    {
        public SignUpViewModel()
        {
            JobRoleIds = new List<int>();
            TechnologyIds = new List<int>();
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

        [StringLength(30)]
        [DataType(DataType.Text)]
        [DisplayName("Team Name:")]
        public string TeamName { get; set; }

        [StringLength(50)]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone:")]
        public string PhoneNumber { get; set; }

        [DisplayName("Shirt Size:")]
        public string ShirtSize { get; set; }

        [DisplayName("Shirt Style:")]
        public string ShirtStyle { get; set; }

        [DisplayName("I am primarily a:")]
        public IList<int> JobRoleIds { get; set; }

        [DisplayName("Technologies you are proficient with:")]
        public IList<int> TechnologyIds { get; set; }

        [DisplayName("I am a student:")]
        public bool IsStudent { get; set; }

        [StringLength(100)]
        [DataType(DataType.Text)]
        [DisplayName("Your day job (e.g. write ASP.NET code):")]
        public string JobDescription { get; set; }

        [DisplayName("I have a laptop:")]
        public bool HasLaptop { get; set; }

        [DisplayName("I have an extra laptop:")]
        public bool HasExtraLaptop { get; set; }

        [DisplayName("I am a GUI designer:")]
        public bool IsGoodGuiDesigner { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Experience Level:")]
        public int ExperienceLevel { get; set; }

        [Required]
        [DisplayName("Years of Software Development Experience:")]
        public int? YearsOfExperience { get; set; }

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
        [DisplayName("Comments:")]
        public string Comments { get; set; }

    }
}
