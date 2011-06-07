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
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [StringLength(30)]
        [DataType(DataType.Text)]
        public string TeamName { get; set; }

        [StringLength(50)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Telephone numbers should be made up of integers")]
        public string PhoneNumber { get; set; }

        public string ShirtSize { get; set; }
        public string ShirtStyle { get; set; }
        public IList<int> JobRoleIds { get; set; }
        public IList<int> TechnologyIds { get; set; }
        public bool IsStudent { get; set; }

        [StringLength(100)]
        [DataType(DataType.Text)]
        public string JobDescription { get; set; }

        public bool HasLaptop { get; set; }
        public bool HasExtraLaptop { get; set; }
        public bool IsGoodGuiDesigner { get; set; }

        [DataType(DataType.Text)]
        public int ExperienceLevel { get; set; }

        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Enter your experience as an integer")]
        public int? YearsOfExperience { get; set; }

        [StringLength(50)]
        [DataType(DataType.Text)]
        public string DietaryNeeds { get; set; }

        [StringLength(50)]
        [DataType(DataType.Text)]
        public string TwitterHandle { get; set; }

        [StringLength(4000)]
        [DataType(DataType.MultilineText)]
        public string Bio { get; set; }

        [StringLength(4000)]
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

    }
}
