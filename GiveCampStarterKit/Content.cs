using System;

namespace GiveCampStarterKit
{
    public class Content
    {
        public int Id { get; set; }
        public bool IsPublished { get; set; }
        public string Title { get; set; }
        public string ContentText { get; set; }
        public string Slug { get; set; }
        public string Tag { get; set; }
        public DateTime PostDate { get; set; }
        public int? AuthorId { get; set; }

        public override string ToString()
        {
            return ContentText;
        }
    }
}
