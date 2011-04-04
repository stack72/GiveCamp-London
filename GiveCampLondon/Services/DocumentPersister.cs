using System;
using System.Configuration;
using System.IO;

namespace GiveCampLondon.Services
{
    public class DocumentPersister : IDocumentPersister
    {
        private static string _documentRepositoryPath;
        
        public DocumentPersister()
        {
            _documentRepositoryPath = ConfigurationManager.AppSettings["DocumentRepositoryPath"];

			if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _documentRepositoryPath)))
				Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _documentRepositoryPath));

        }


        #region IDocumentPersister Members

        public  System.IO.Stream ReadFile(IDocument document)
        {

			return new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\" + _documentRepositoryPath, document.LocalFilename),
                                              FileMode.Open, 
                                              FileAccess.Read);
        }

        public void WriteFile(IDocument document, Stream documentStream)
        {

            int readByte = documentStream.ReadByte();
			var outputStream = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\" + _documentRepositoryPath, document.LocalFilename),
                                              FileMode.Create);        
               
            while( readByte != -1)
            {
                outputStream.WriteByte((byte)readByte);
                readByte = documentStream.ReadByte();
            }

            outputStream.Close();
            outputStream.Dispose();
        }

        public void DeleteFile(IDocument document)
        {
			File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\" + _documentRepositoryPath, document.LocalFilename));
        }

        #endregion
    }
}
