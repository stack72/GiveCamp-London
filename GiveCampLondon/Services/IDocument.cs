using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GiveCampLondon.Services
{
    public interface IDocument
    {
        string LocalFilename { get; set; }
    }
}
