using System;
using NUnit.Framework;
using GiveCampStarterKit.Repositories;

namespace GiveCampStarterKit.Tests.IntegrationTests.Repositories
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
