using System;
using System.Data.SqlServerCe;
using System.IO;
using GiveCampLondon.Tests.IntegrationTests.Repositories;
using NUnit.Framework;

namespace GiveCampLondon.Tests.SQLCE_Helper
{
    [TestFixture]
    public class SQLCETests : BaseRepositoryTest
    {
        public SQLCETests()
            : base(SQLCEConnectionStringName)
        {

        }

        [Test]
        public void TestBasicQuery()
        {

            if (!File.Exists(DBFilePath))
                throw new ApplicationException("No database!");

            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            try
            {
                cn.Open();
				
                using (var reader = new SqlCeCommand("SELECT * FROM Content", cn).ExecuteReader())
                {
                    if (!reader.Read())
                        throw new ApplicationException("No data found");
                }
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
