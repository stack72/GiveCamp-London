using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GiveCampLondon
{
    public class Charity
    {
        

        [ScaffoldColumn(false)]
        public int Id { get; set; }
        
        [ScaffoldColumn(false)]
        public Guid MembershipId { get; set; }
        
        [DisplayName("Charity Email")]
        public string Email { get; set; }

        [DisplayName("Charity Name")]
        public string CharityName { get; set; }
        
        [DisplayName("Background Information")]
        public string BackgroundInformation { get; set; }
        
        [DisplayName("Work Requested")]
        public string WorkRequested { get; set; }

        [DisplayName("Other Infrastructure")]
        public string OtherInfrastructure { get; set; }
        
        [DisplayName("Other Support Skills")]
        public string OtherSupportSkills { get; set; }

        [DisplayName("Website")]
        public string Website { get; set; }

        [DisplayName("ContactName")]
        public string ContactName { get; set; }

        [DisplayName("ContactPhone")]
        public string ContactPhone { get; set; }

        [ScaffoldColumn(false)]
        public bool Approved { get; set; }

        public string About { get; set; }
        public string LogoPath { get; set; }
    }
}
