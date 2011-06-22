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
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [StringLength(50)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Telephone numbers should be made up of integers")]
        public string PhoneNumber { get; set; }
        public IList<int> NonTechJobRoleIds { get; set; }

        [StringLength(100)]
        [DataType(DataType.Text)]
        public string JobDescription { get; set; }
        public string ShirtSize { get; set; }
        public string ShirtStyle { get; set; }

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
        public string SkillsOutline { get; set; }

        [StringLength(4000)]
        [DataType(DataType.MultilineText)]
        public string ExpertiseTopic { get; set; }
        public IList<int> Availability { get; set; }
        public bool IsOnWaitList { get; set; }
    }
}
