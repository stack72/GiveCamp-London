using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlServerCe;
using System.IO;

namespace GiveCampLondon.Tests.IntegrationTests.Repositories
{
    public abstract class BaseRepositoryTest
    {
        public const string SQLConnectionStringName = "SiteDataContext";
        public const string SQLCEConnectionStringName = "SiteDataContextCE";

        public BaseRepositoryTest()
            : this(SQLConnectionStringName)
        {

        }

        public BaseRepositoryTest(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
            this.ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

            if (connectionStringName == SQLCEConnectionStringName)
                CreateTestDatabaseIfNotExists();
        }

        protected string ConnectionStringName;
        protected string ConnectionString;

        private const string rootFileLocation = @"..\..\..\"; //assumes tests will run in bin\debug folder
        protected string DBFilePath
        {
            get
            {
                int firstPos = ConnectionString.IndexOf("'");
                string retVal = ConnectionString.Substring(firstPos + 1, ConnectionString.IndexOf("'", firstPos + 1) - firstPos - 1);

                return retVal;
            }
        }

        public void CreateTestDatabaseIfNotExists()
        {
            string buildSQLPath = rootFileLocation + @"Build\SqlCESchema.sql";

            if (File.Exists(DBFilePath))
                return;

            SqlCeEngine en = new SqlCeEngine(ConnectionString);
            en.CreateDatabase();

            string sql = File.ReadAllText(buildSQLPath);

            //no batch support in SQL CE so try to separate line per semicolon
            string[] commands = sql.Split(';');

            SqlCeConnection cn = new SqlCeConnection(ConnectionString);
            try
            {
                cn.Open();

                foreach (string command in commands.Where(x => !String.IsNullOrEmpty((x ?? "").Trim())))
                {
                    try
                    {
                        new SqlCeCommand(command, cn).ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error running command: " + command, ex);
                    }
                }
            }
            finally
            {
                cn.Close();
            }
        }
    }

    //this is necessary because CF caches per datacontext type and throws exceptions if multiple providers for same type
    public static class SiteDataContextFactory
    {
        public static SiteDataContext FromName(string connectionStringName)
        {
            if (connectionStringName == "SiteDataContext")
                return new SiteDataContextSQL();
            else
                return new SiteDataContextSQLCE();
        }
    }

    public class SiteDataContextSQL : SiteDataContext
    {
        public SiteDataContextSQL()
            : base("SiteDataContext")
        {
        }
    }

    public class SiteDataContextSQLCE : SiteDataContext
    {
        public SiteDataContextSQLCE()
            : base("SiteDataContextCE")
        {
        }
    }

}
