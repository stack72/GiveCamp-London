using System.ComponentModel.DataAnnotations;

namespace GiveCampLondon
{
    public class Sponsor
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Blurb { get; set; }
        public string MainLogo { get; set; }
        public string SmallLogo { get; set; }
        public string Link { get; set; }
    }
}