using System.IO;

namespace GiveCampLondon.Services
{
    public interface IDocumentPersister
    {
        Stream ReadFile(IDocument document);
        void WriteFile(IDocument document, Stream documentStream);
        void DeleteFile(IDocument document);
    }
}
