using System;
using NUnit.Framework;
using GiveCampLondon.Repositories;

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
