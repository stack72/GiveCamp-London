using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveCampLondon
{
    public class Setting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public int Version { get; set; }
        public string TwitterTag { get; set; }
        public bool PublishCharities { get; set; }
        public bool PublishVolunteers { get; set; }

    }
}
