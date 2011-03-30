using System;
using System.Collections.Generic;
using NUnit.Framework;
using GiveCampStarterKit.Repositories;

namespace GiveCampStarterKit.Tests.IntegrationTests.Repositories
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
