using System.Collections.Generic;

namespace GiveCampLondon.Website.Models.Volunteer
{
    public class VolunteerSummaryModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TeamName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TwitterHandle { get; set; }

    }

    public class TechieSummary
    {
        public IEnumerable<VolunteerSummaryModel> Volunteers { get; set; }

        public int TotalSignups { get; set; }
        public int RegisteredTechies { get; set; }
        public int OnWaitListVolunteers { get; set; }
    }
}