using System;
using GiveCampLondon.Repositories;
using NUnit.Framework;

namespace GiveCampLondon.Tests.IntegrationTests.Repositories
{
    [TestFixture]
    public class CharityRepositoryTests : BaseRepositoryTest
    {
        public CharityRepositoryTests() : this(SQLConnectionStringName) { }
        public CharityRepositoryTests(string connectionStringName) : base(connectionStringName) { }

        [Test]
        public void Crud()
        {
            var repo = new CharityRepository(SiteDataContextFactory.FromName(ConnectionStringName));

            var charity = new Charity
            {
                CharityName = "Bob's House",
                BackgroundInformation = "teh backgroundz",
                OtherInfrastructure = "i need teh serverz",
                OtherSupportSkills = "i need teh skillz",
                WorkRequested = "i neez teh slavez",
                Email = Guid.NewGuid().ToString() + "@foo.com"
            };
            repo.Save(charity);

            var retrievedCharity = repo.Get(charity.Id);
            Assert.IsNotNull(retrievedCharity, "Was not able to get the charity.");

            retrievedCharity.BackgroundInformation = "teh other backgroundz";
            repo.Save(retrievedCharity);

            var updatedCharity = repo.Get(charity.Id);
            Assert.AreEqual("teh other backgroundz", updatedCharity.BackgroundInformation, "The charity was not updated.");

            repo.Delete(updatedCharity);
            var deletedCharity = repo.Get(updatedCharity.Id);
            Assert.IsNull(deletedCharity, "The charity was not deleted. Doh!");
        }
    }
}
