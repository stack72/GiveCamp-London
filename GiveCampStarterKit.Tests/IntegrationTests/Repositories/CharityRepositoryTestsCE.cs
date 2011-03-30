using System;
using NUnit.Framework;
using GiveCampStarterKit.Repositories;

namespace GiveCampStarterKit.Tests.IntegrationTests.Repositories
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
