using System;
using System.Collections.Generic;
using NUnit.Framework;
using GiveCampStarterKit.Repositories;

namespace GiveCampStarterKit.Tests.IntegrationTests.Repositories
{

    [TestFixture]
    public class VolunteerRepositoryTests : BaseRepositoryTest
    {
        public VolunteerRepositoryTests() : this(SQLConnectionStringName) { }
        public VolunteerRepositoryTests(string connectionStringName) : base(connectionStringName) { }
        private ExperienceLevelRepository xpRepo;
        private VolunteerRepository repo;
        private Volunteer volunteer;
        private JobRoleRepository jobRoleRepo;
        private TechnologyRepository technologyRepo;

        [SetUp]
        public void SetUp()
        {
            var context = SiteDataContextFactory.FromName(ConnectionStringName);
            xpRepo = new ExperienceLevelRepository(context);
            repo = new VolunteerRepository(context);
            jobRoleRepo = new JobRoleRepository(context);
            technologyRepo = new TechnologyRepository(context);

            volunteer = CreateVolunteer(xpRepo, repo);
        }

        [Test]
        public void Crud()
        {
            var retrievedVolunteer = repo.Get(volunteer.Id);
            Assert.IsNotNull(retrievedVolunteer, "Was not able to get the volunteer.");
            Assert.AreEqual(1, retrievedVolunteer.ExperienceLevel.Id, "Was not able to get the xp level.");

            retrievedVolunteer.FirstName = "Another Tester";
            retrievedVolunteer.ExperienceLevel = xpRepo.Get(2);
            repo.Save(retrievedVolunteer);
            var updatedVolunteer = repo.Get(volunteer.Id);
            Assert.IsNotNull(updatedVolunteer, "Was not able to update the volunteer.");
            Assert.AreEqual("Another Tester", updatedVolunteer.FirstName, "The volunteer was not updated.");
            Assert.AreEqual(2, retrievedVolunteer.ExperienceLevel.Id, "Was not able to get the xp level.");

            repo.Delete(updatedVolunteer);
            var deletedVolunteer = repo.Get(updatedVolunteer.Id);
            Assert.IsNull(deletedVolunteer, "The volunteer was not deleted. Doh!");
        }

        private Volunteer CreateVolunteer(ExperienceLevelRepository xpRepo, VolunteerRepository repo)
        {
            var volunteer = new Volunteer
                                {
                                    Bio = "blah",
                                    Comments = "none",
                                    DietaryNeeds = "MEAT!",
                                    ExperienceLevel = xpRepo.Get(1),
                                    FirstName = "Tester",
                                    HasExtraLaptop = true,
                                    JobDescription = "tester",
                                    LastName = "McTest",
                                    PhoneNumber = "9999999999",
                                    TeamName = "Testers",
                                    Email = "tester@test.com"
                                };
            repo.Save(volunteer);
            return volunteer;
        }

        [Test]
        public void CrudWithJobRoles()
        {
            volunteer.JobRoles = new List<JobRole>() {jobRoleRepo.Get(1), jobRoleRepo.Get(2), jobRoleRepo.Get(3)};
            repo.Save(volunteer);

            var retrievedVolunteer = repo.Get(volunteer.Id);
            var volunteerJobRoles = repo.FindJobRolesFor(retrievedVolunteer.Id);
            Assert.IsNotNull(retrievedVolunteer, "Was not able to get the volunteer.");
            Assert.AreEqual(3, retrievedVolunteer.JobRoles.Count, "Was not able to get with jobroles.");
            Assert.AreEqual(3, volunteerJobRoles.Count, "Was not able to get volunteerjobroles.");

            retrievedVolunteer.FirstName = "Another Tester";
            repo.Save(retrievedVolunteer);
            volunteerJobRoles = repo.FindJobRolesFor(retrievedVolunteer.Id);
            var updatedVolunteer = repo.Get(volunteer.Id);
            Assert.IsNotNull(updatedVolunteer, "Was not able to update the volunteer.");
            Assert.AreEqual("Another Tester", updatedVolunteer.FirstName, "The volunteer was not updated.");
            Assert.AreEqual(3, updatedVolunteer.JobRoles.Count, "Was not able to remove the job role.");
            Assert.AreEqual(3, volunteerJobRoles.Count, "Was not able to volunteerjobroles.");

            updatedVolunteer.JobRoles.RemoveAt(1);
            repo.Save(updatedVolunteer);
            updatedVolunteer = repo.Get(volunteer.Id);
            volunteerJobRoles = repo.FindJobRolesFor(updatedVolunteer.Id);
            Assert.IsNotNull(updatedVolunteer, "Was not able to remove the job role.");
            Assert.AreEqual(2, updatedVolunteer.JobRoles.Count, "Was not able to remove the job role.");
            Assert.AreEqual(2, volunteerJobRoles.Count, "Was not able to volunteerjobroles.");

            updatedVolunteer.JobRoles.Add(jobRoleRepo.Get(4));
            repo.Save(updatedVolunteer);
            updatedVolunteer = repo.Get(volunteer.Id);
            volunteerJobRoles = repo.FindJobRolesFor(updatedVolunteer.Id);
            Assert.IsNotNull(updatedVolunteer, "Was not able to add the job role.");
            Assert.AreEqual(3, updatedVolunteer.JobRoles.Count, "Was not able to add the job role.");
            Assert.AreEqual(3, volunteerJobRoles.Count, "Was not able to volunteerjobroles.");

            repo.Delete(updatedVolunteer);
            var deletedVolunteer = repo.Get(updatedVolunteer.Id);
            volunteerJobRoles = repo.FindJobRolesFor(updatedVolunteer.Id);
            Assert.IsNull(deletedVolunteer, "The volunteer was not deleted. Doh!");
            Assert.AreEqual(0, volunteerJobRoles.Count, "The volunteerjobroles were not deleted. Doh!");
        }

        [Test]
        public void CrudWithTechnologies()
        {
            volunteer.Technologies = new List<Technology>() {technologyRepo.Get(1), technologyRepo.Get(2), technologyRepo.Get(3)};
            repo.Save(volunteer);

            var retrievedVolunteer = repo.Get(volunteer.Id);
            var volunteerTechnologies = repo.FindTechnologiesFor(retrievedVolunteer.Id);
            Assert.IsNotNull(retrievedVolunteer, "Was not able to get the volunteer.");
            Assert.AreEqual(3, retrievedVolunteer.Technologies.Count, "Was not able to get with technologies.");
            Assert.AreEqual(3, volunteerTechnologies.Count, "Was not able to get volunteertechnologies.");

            retrievedVolunteer.FirstName = "Another Tester";
            repo.Save(retrievedVolunteer);
            volunteerTechnologies = repo.FindTechnologiesFor(retrievedVolunteer.Id);
            var updatedVolunteer = repo.Get(volunteer.Id);
            Assert.IsNotNull(updatedVolunteer, "Was not able to update the volunteer.");
            Assert.AreEqual("Another Tester", updatedVolunteer.FirstName, "The volunteer was not updated.");
            Assert.AreEqual(3, updatedVolunteer.Technologies.Count, "Was not able to get with technologies.");
            Assert.AreEqual(3, volunteerTechnologies.Count, "Was not able to get volunteertechnologies.");

            updatedVolunteer.Technologies.RemoveAt(1);
            repo.Save(updatedVolunteer);
            updatedVolunteer = repo.Get(volunteer.Id);
            volunteerTechnologies = repo.FindTechnologiesFor(updatedVolunteer.Id);
            Assert.IsNotNull(updatedVolunteer, "Was not able to remove the job role.");
            Assert.AreEqual(2, updatedVolunteer.Technologies.Count, "Was not able to get with technologies.");
            Assert.AreEqual(2, volunteerTechnologies.Count, "Was not able to get volunteertechnologies.");

            updatedVolunteer.Technologies.Add(technologyRepo.Get(4));
            repo.Save(updatedVolunteer);
            updatedVolunteer = repo.Get(volunteer.Id);
            volunteerTechnologies = repo.FindTechnologiesFor(updatedVolunteer.Id);
            Assert.IsNotNull(updatedVolunteer, "Was not able to add the job role.");
            Assert.AreEqual(3, updatedVolunteer.Technologies.Count, "Was not able to get with technologies.");
            Assert.AreEqual(3, volunteerTechnologies.Count, "Was not able to get volunteertechnologies.");

            repo.Delete(updatedVolunteer);
            var deletedVolunteer = repo.Get(updatedVolunteer.Id);
            volunteerTechnologies = repo.FindTechnologiesFor(updatedVolunteer.Id);
            Assert.IsNull(deletedVolunteer, "The volunteer was not deleted. Doh!");
            Assert.AreEqual(0, volunteerTechnologies.Count, "The volunteertechnologies were not deleted. Doh!");
        }

    }
}
