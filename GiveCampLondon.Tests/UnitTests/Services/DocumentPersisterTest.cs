using System;
using System.Configuration;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GiveCampLondon.Services;
using System.IO;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace GiveCampLondon.Tests.UnitTests.Services
{
    [TestFixture]
    public class DocumentPersisterTest
    {

        private string _documentRepositoryPath; 
        
        [TestFixtureSetUp]
        public void Setup()
        {
            //_documentRepositoryPath = Path.Combine(Assembly.GetExecutingAssembly().Location,
            //                               ConfigurationManager.AppSettings["DocumentRepositoryPath"]);

            _documentRepositoryPath = ConfigurationManager.AppSettings["DocumentRepositoryPath"];
        }
       
        [Test]
        public void CreateRepositoryDirectory()
        {
            var persister = new DocumentPersister();

            Assert.True(Directory.Exists(_documentRepositoryPath),
                          string.Format("Failed to create repository directory {0}", _documentRepositoryPath));

        }

        [Test]
        public void Write()
        {
            var filename = Path.Combine(_documentRepositoryPath, "Test.txt");
            var testDocument = new Document() { LocalFilename = "Test.txt" };
            //var x = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GiveCampStarterKit.Tests.UnitTests.Services.SampleFile.txt");
            var persister = new DocumentPersister();

            if(File.Exists(filename)) File.Delete(filename);

            persister.WriteFile(testDocument, fileStream);

            Assert.True(File.Exists(filename));


            fileStream.Dispose();

        }

        [Test]
        public void Read()
        {

            // save the initial file
            var filename = Path.Combine(_documentRepositoryPath, "Test.txt");
            var originalStream =
                Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    "GiveCampStarterKit.Tests.UnitTests.Services.SampleFile.txt");
            var fileReader = new StreamReader(originalStream);
            var originalText = fileReader.ReadToEnd();
            fileReader.Close();
            originalStream.Close();

            var fileWriter = new StreamWriter(filename, false);
            fileWriter.Write(originalText);
            fileWriter.Close();


            var persister = new DocumentPersister();

            var testDocument = new Document() {LocalFilename = "Test.txt"};
            var returnStream = persister.ReadFile(testDocument);
            var returnReader = new StreamReader(returnStream);
            var returnText = returnReader.ReadToEnd();
            returnReader.Close();
            returnStream.Close();

            // quick and dirty check
            Assert.AreEqual(originalText, returnText);


            originalStream.Dispose();
            fileReader.Dispose();
            fileWriter.Dispose();
            returnReader.Dispose();
            returnStream.Dispose();
        }
    }
}
