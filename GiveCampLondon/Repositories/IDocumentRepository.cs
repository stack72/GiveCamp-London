using System.Collections.Generic;
using System.IO;

namespace GiveCampLondon.Repositories
{
    public interface IDocumentRepository
    {
        List<Document> GetAll();
        List<Document> GetAllByType(string documentType);
        Document Get(int id);
        Stream GetFile(Document document);
        void Save(Document document);
        void Save(Document document, Stream documentStream);
        void Delete(Document document);
    }
}
