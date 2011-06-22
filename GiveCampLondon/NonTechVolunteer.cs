using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GiveCampLondon
{
    public class NonTechVolunteer : User
    {
        public NonTechVolunteer()
        {
            AreasOfExpertise = new List<Expertise>();
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

        [DisplayName("Job Description:")]
        public string JobDescription { get; set; }

        [DisplayName("Expertise:")]
        public IList<Expertise> AreasOfExpertise { get; set; }

        [DisplayName("Dietary Needs:")]
        public string DietaryNeeds { get; set; }

        [DisplayName("Twitter Handle:")]
        public string TwitterHandle { get; set; }

        [DisplayName("Bio:")]
        public string Bio { get; set; }

        [DisplayName("Shirt Size:")]
        public string ShirtSize { get; set; }

        [DisplayName("Shirt Style:")]
        public string ShirtStyle { get; set; }

        [DisplayName("Skill Set:")]
        public string SkillSet { get; set; }

        [DisplayName("Session Details:")]
        public string SessionDetails { get; set; }

        public bool IsOnWaitList{ get; set; }
    }
}