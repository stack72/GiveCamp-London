using System;
using System.Linq;
using NUnit.Framework;
using GiveCampLondon.Repositories;

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
