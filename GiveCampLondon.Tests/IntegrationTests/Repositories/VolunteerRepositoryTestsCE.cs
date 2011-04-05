using NUnit.Framework;

namespace GiveCampLondon.Tests.IntegrationTests.Repositories
{
    [TestFixture]
    public class VolunteerRepositoryTestsCE : VolunteerRepositoryTests
    {
        public VolunteerRepositoryTestsCE()
            : base(SQLCEConnectionStringName)
        {

        }
    }
}
