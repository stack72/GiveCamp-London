using NUnit.Framework;

namespace GiveCampLondon.Tests.IntegrationTests.Repositories
{
    [TestFixture]
    public class ContentRepositoryTestsCE : ContentRepositoryTests
    {
        public ContentRepositoryTestsCE()
            : base(SQLCEConnectionStringName)
        {

        }
    }
}
