using NUnit.Framework;

namespace GiveCampLondon.Tests.IntegrationTests.Repositories
{
    [TestFixture]
    public class TeamRepositoryTestsCE : TeamRepositoryTests
    {
        public TeamRepositoryTestsCE()
            : base(SQLCEConnectionStringName)
        {

        }
    }
}
