using System;
using NUnit.Framework;
using GiveCampLondon.Repositories;

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
