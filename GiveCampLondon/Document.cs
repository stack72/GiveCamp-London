using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GiveCampLondon.Services;

namespace GiveCampLondon
{
    public class Document : IDocument
    {
        public int DocumentId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OriginalFilename { get; set; }
        public string MimeType { get; set; }
        public string LocalFilename { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
