using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GiveCampLondon.Repositories;
using GiveCampLondon.Services;
using NUnit.Framework;


namespace GiveCampLondon.Tests.IntegrationTests.Repositories
{
    [TestFixture]
    public class DoumentRepositoryTest : BaseRepositoryTest
    {
        public DoumentRepositoryTest() : this(SQLConnectionStringName) { }
        public DoumentRepositoryTest(string connectionStringName) : base(connectionStringName) { }

        private DocumentRepository _docmentRepository;
        private string _documentRepositoryPath; 
        
        [TestFixtureSetUp]
        public void Setup ()
        {
            _docmentRepository = new DocumentRepository(new SiteDataContext(), new DocumentPersister());
            _documentRepositoryPath = ConfigurationManager.AppSettings["DocumentRepositoryPath"];
 
        }


        [Test]
        public void DbSaveGet()
        {
             
            var expected = new Document()
                               {
                                   Description = "Description",
                                   DocumentId = 0,
                                   MimeType = "MimeType",
                                   Name = "Name",
                                   OriginalFilename = "OriginalFilename",
                                   Type = "Type",
                                   UploadDate = DateTime.Now
                               };

            _docmentRepository.Save(expected);

            var actual = _docmentRepository.Get(expected.DocumentId);

            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.DocumentId, expected.DocumentId);
            Assert.AreEqual(actual.Description, expected.Description);
            Assert.IsNotNullOrEmpty(actual.LocalFilename);
            Assert.AreEqual(actual.MimeType, expected.MimeType);
            Assert.AreEqual(actual.Name, expected.Name);
            Assert.AreEqual(actual.OriginalFilename, expected.OriginalFilename);
            Assert.AreEqual(actual.Type, expected.Type);
            Assert.AreEqual(actual.UploadDate, expected.UploadDate);
        }

        [Test]
        public void DbUpdate()
        {

            var original = _docmentRepository.GetAll().FirstOrDefault();
            var originalName = original.Name;
            original.Name = original.Name + "Test";

            _docmentRepository.Save(original);

            var updated = _docmentRepository.Get(original.DocumentId);

            Assert.IsNotNull(updated);
            Assert.AreEqual(updated.DocumentId, original.DocumentId);
            Assert.AreNotEqual(updated.Name, originalName);
        }

        [Test]
        public void FileCrud()
        {

            var fileStream =
                Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    "GiveCampStarterKit.Tests.UnitTests.Services.SampleFile.txt");

            var document = new Document()
                               {
                                   Description = "Description",
                                   MimeType = "MimeType",
                                   Name = "Name",
                                   OriginalFilename = "OriginalFilename",
                                   Type = "Type",
                                   UploadDate = DateTime.Now
                               };

            _docmentRepository.Save(document, fileStream);

            Assert.IsNotNullOrEmpty(document.LocalFilename);
            Assert.True(File.Exists(Path.Combine(_documentRepositoryPath, document.LocalFilename)));

            _docmentRepository.Delete(document);

            var removedDocument = _docmentRepository.Get(document.DocumentId);
            Assert.IsNull(removedDocument);
            Assert.False(File.Exists(Path.Combine(_documentRepositoryPath, document.LocalFilename)));           

        }
    }
}
