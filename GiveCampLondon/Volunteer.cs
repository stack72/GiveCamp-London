using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GiveCampLondon
{
    public class Volunteer : User
    {
        public Volunteer()
        {
            JobRoles = new List<JobRole>();
            Technologies = new List<Technology>();
        }
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        
        [DisplayName("First Name:")]
        public string FirstName { get; set; }
        
        [DisplayName("Last Name:")]
        public string LastName { get; set; }

        [DisplayName("Phone:")]
        public string PhoneNumber { get; set; }
        
        [DisplayName("Email Address:")]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public Guid MembershipId { get; set; }

        [DisplayName("Team Preference:")]
        public string TeamName { get; set; }

        [DisplayName("Is a student:")]
        public bool IsStudent { get; set; }

        [DisplayName("Job Description:")]
        public string JobDescription { get; set; }

        [DisplayName("Job Roles:")]
        public IList<JobRole> JobRoles { get; set; }

        [DisplayName("Technologies:")]
        public IList<Technology> Technologies { get; set; }

        [DisplayName("Has a laptop:")]
        public bool HasLaptop { get; set; }

        [DisplayName("Has an extra laptop:")]
        public bool HasExtraLaptop { get; set; }

        [DisplayName("Is a good GUI designer:")]
        public bool IsGoodGuiDesigner { get; set; }

        [DisplayName("Experience Level:")]
        public ExperienceLevel ExperienceLevel { get; set; }

        [DisplayName("Years of Experience:")]
        public int? YearsOfExperience { get; set; }

        [DisplayName("Dietary Needs:")]
        public string DietaryNeeds { get; set; }

        [DisplayName("Twitter Handle:")]
        public string TwitterHandle { get; set; }

        [DisplayName("Bio:")]
        public string Bio { get; set; }

        [DisplayName("Comments:")]
        public string Comments { get; set; }

        [DisplayName("Shirt Size:")]
        public string ShirtSize { get; set; }

        [DisplayName("Shirt Style:")]
        public string ShirtStyle { get; set; }

        [DisplayName("Assigned Team:")]
        public int? TeamId { get; set; }

        public bool IsOnWaitList { get; set; }
        public bool HasCancelled { get; set; }
    }

    public class ShirtSizeValues
    {
        static ShirtSizeValues()
        {
            ShirtSizes = new List<string>
                             {
                                 None,
                                 S,
                                 M,
                                 L,
                                 XL,
                                 XXL,
                                 XXXL
                             };
        }
        public static List<string> ShirtSizes;
        public const string None = "";
        public const string S = "S";
        public const string M = "M";
        public const string L = "L";
        public const string XL = "XL";
        public const string XXL = "2XL";
        public const string XXXL = "3XL";
    }

    public class ShirtStyleValues
    {
        static ShirtStyleValues()
        {
            ShirtStyles = new List<string>
                              {
                                 None,
                                 Mens,
                                 Womens
                             };
        }
        public static List<string> ShirtStyles;
        public const string None = "";
        public const string Mens = "Mens";
        public const string Womens = "Womens";
    }

}