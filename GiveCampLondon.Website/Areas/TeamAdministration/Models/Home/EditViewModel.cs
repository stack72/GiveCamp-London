using System;
using System.Collections.Generic;

namespace GiveCampLondon.Website.Areas.TeamAdministration.Models.Home
{
    public class EditViewModel
    {
        public EditViewModel()
        {
            Volunteers = new List<Volunteer>();
            OtherVolunteers = new List<Volunteer>();
        }

        public Team Team { get; set; }
        public List<Volunteer> Volunteers { get; set; }
        public List<Volunteer> OtherVolunteers { get; set; }
    }
}