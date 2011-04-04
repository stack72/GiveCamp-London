using System;
using System.Collections.Generic;
using System.Linq;
using GiveCampLondon.Services;

namespace GiveCampLondon.Repositories
{
    public class DocumentRepository:IDocumentRepository
    {

        private SiteDataContext _dataContext;
        private DocumentPersister _documentPersister;

        public DocumentRepository(SiteDataContext dataContext, DocumentPersister documentPersister)
        {
            _dataContext = dataContext;
            _documentPersister = documentPersister;
        }

        #region IDocumentRepository Members

        public List<Document> GetAll()
        {
            return _dataContext.Documents.ToList();
        }

        public List<Document> GetAllByType(string documentType)
        {
            return _dataContext.Documents.Where(d=>d.Type == documentType).ToList();
        }

        public Document Get(int id)
        {
            return _dataContext.Documents.FirstOrDefault(d => d.DocumentId == id);
        }

        public System.IO.Stream GetFile(Document document)
        {
            return _documentPersister.ReadFile(document);
        }

        public void Save(Document document)
        {
            if (document.DocumentId == 0)
            {
                document.LocalFilename = Guid.NewGuid().ToString();
                _dataContext.Documents.Add(document);
            }

            _dataContext.SaveChanges();
        }

        public void Save(Document document, System.IO.Stream documentStream)
        {
            if (document.DocumentId == 0)
            {
                document.LocalFilename = Guid.NewGuid().ToString();
                _dataContext.Documents.Add(document);
            }

            _dataContext.SaveChanges();

            _documentPersister.WriteFile(document,documentStream);
        }

        public void Delete(Document document)
        {
            _dataContext.Documents.Remove(document);
            _dataContext.SaveChanges();

            _documentPersister.DeleteFile(document);
        }

        #endregion
    }
}
