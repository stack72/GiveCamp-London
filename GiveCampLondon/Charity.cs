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
        public string Name { get; set; }
        
        [DisplayName("Background Information")]
        public string BackgroundInformation { get; set; }
        
        [DisplayName("Work Requested")]
        public string WorkRequested { get; set; }

        [DisplayName("Other Infrastructure")]
        public string OtherInfrastructure { get; set; }
        
        [DisplayName("Other Support Skills")]
        public string OtherSupportSkills { get; set; }

        [ScaffoldColumn(false)]
        public bool Approved { get; set; }
    }
}
