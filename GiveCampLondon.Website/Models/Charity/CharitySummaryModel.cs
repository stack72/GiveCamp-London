using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiveCampLondon.Website.Models.Charity
{
    public class CharitySummaryModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Approved { get; set; }
        public int Id { get; set; }
    }
}