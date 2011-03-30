using System;
using System.Linq;
using NUnit.Framework;
using GiveCampStarterKit.Repositories;

namespace GiveCampStarterKit.Tests.IntegrationTests.Repositories
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
