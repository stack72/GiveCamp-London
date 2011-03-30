using System;
using NUnit.Framework;
using GiveCampLondon.Repositories;

namespace GiveCampLondon.Tests.IntegrationTests.Repositories
{
    [TestFixture]
    public class TeamRepositoryTests : BaseRepositoryTest
    {
        [SetUp]
        public void SetUp()
        {
            _repo = new TeamRepository(SiteDataContextFactory.FromName(ConnectionStringName));
        }

        private TeamRepository _repo;

        public TeamRepositoryTests() : this(SQLConnectionStringName) { }
        public TeamRepositoryTests(string connectionStringName) : base(connectionStringName) { }

        [Test]
        public void Crud()
        {
            var team = new Team { Name = "Cool Team " + Guid.NewGuid().ToString() };

            _repo.Save(team);
            var retrievedTeam = _repo.Get(team.Id);
            Assert.IsNotNull(retrievedTeam, "Did not get the team. Might not have saved, yo.");

            retrievedTeam.Name = "Changin it yo " + Guid.NewGuid().ToString();
            _repo.Save(retrievedTeam);
            var updatedTeam = _repo.Get(team.Id);
            Assert.AreEqual(retrievedTeam.Name, updatedTeam.Name, "The team was not updated.");

            _repo.Delete(updatedTeam);
            var deletedTeam = _repo.Get(team.Id);
            Assert.IsNull(deletedTeam, "The team was not updated.");
        }

        [Test]
        public void CanGetListOfTeams()
        {
            var team0 = new Team { Name = "Cool Team " + Guid.NewGuid().ToString() };
            var team1 = new Team { Name = "Cool Team " + Guid.NewGuid().ToString() };
            var team2 = new Team { Name = "Cool Team " + Guid.NewGuid().ToString() };

            _repo.Save(team0);
            _repo.Save(team1);
            _repo.Save(team2);

            var teams = _repo.GetAll();
            Assert.IsTrue(teams.Count >= 3, "Was not able to fetch teams.");

            _repo.Delete(team0);
            _repo.Delete(team1);
            _repo.Delete(team2);
        }
    }
}
