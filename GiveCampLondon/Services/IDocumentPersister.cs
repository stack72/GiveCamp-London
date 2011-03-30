using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GiveCampLondon.Services
{
    public interface IDocumentPersister
    {
        Stream ReadFile(IDocument document);
        void WriteFile(IDocument document, Stream documentStream);
        void DeleteFile(IDocument document);
    }
}
