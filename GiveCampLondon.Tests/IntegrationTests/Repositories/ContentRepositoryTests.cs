using System;
using System.Linq;
using GiveCampLondon.Repositories;
using NUnit.Framework;

namespace GiveCampLondon.Tests.IntegrationTests.Repositories
{
    [TestFixture]
    public class ContentRepositoryTests : BaseRepositoryTest
    {
        public ContentRepositoryTests() : this(SQLConnectionStringName) { }
        public ContentRepositoryTests(string connectionStringName) : base(connectionStringName) { }

        [Test]
        public void Crud()
        {
            var contentPageRepository = new ContentRepository(SiteDataContextFactory.FromName(ConnectionStringName));

            var content = new Content
            {
                ContentText = "foo",
                IsPublished = false,
                PostDate = DateTime.Now,
                Slug = Guid.NewGuid().ToString(),
                Tag = Guid.NewGuid().ToString(),
                Title = "the foo"
            };

            contentPageRepository.Save(content);

            var retrievedContent = contentPageRepository.Get(content.Id);
            Assert.IsNotNull(retrievedContent, "Did not get the content back.");
            retrievedContent.ContentText = "bar";
            contentPageRepository.Save(retrievedContent);

            var updatedContent = contentPageRepository.Get(content.Id);
            Assert.AreEqual("bar", updatedContent.ContentText, "Content did not get updated.");

            contentPageRepository.Delete(content);
            var deletedContent = contentPageRepository.Get(content.Id);
            Assert.IsNull(deletedContent, "The content should have been deleted but was not.");
        }

        [Test]
        public void GetSlugs()
        {
            var contentPageRepository = new ContentRepository(SiteDataContextFactory.FromName(ConnectionStringName));

            var slugs = contentPageRepository.GetSlugs();
            Assert.IsNotNull(slugs);
            Assert.True(slugs.Count > 0, "Returned zero slugs.");

        }

        [Test]
        public void GetTags()
        {
            var contentPageRepository = new ContentRepository(SiteDataContextFactory.FromName(ConnectionStringName));

            var slug = contentPageRepository.GetSlugs().FirstOrDefault();
            Assert.IsNotNullOrEmpty(slug, "No slug returned");
            var tags = contentPageRepository.GetTags(slug);
            Assert.True(tags.Count > 0, string.Format("No tags returned for slug '{0}'.", slug));
        }
    }
}
