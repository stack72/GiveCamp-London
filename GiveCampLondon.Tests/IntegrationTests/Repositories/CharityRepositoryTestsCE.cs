using NUnit.Framework;

namespace GiveCampLondon.Tests.IntegrationTests.Repositories
{
    [TestFixture]
    public class CharityRepositoryTestsCE : CharityRepositoryTests
    {
        public CharityRepositoryTestsCE()
            : base(SQLCEConnectionStringName)
        {

        }
    }
}
