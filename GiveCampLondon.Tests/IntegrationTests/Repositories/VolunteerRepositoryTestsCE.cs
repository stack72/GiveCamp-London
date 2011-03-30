using System;
using System.Collections.Generic;
using NUnit.Framework;
using GiveCampLondon.Repositories;

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
