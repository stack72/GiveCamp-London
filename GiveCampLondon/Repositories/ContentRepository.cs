using System.Collections.Generic;
using System.Linq;

namespace GiveCampLondon.Repositories
{
    public class ContentRepository : IContentRepository
    {
        public ContentRepository(SiteDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private SiteDataContext _dataContext;

        public List<string> GetSlugs()
        {
            return _dataContext.Content.Select(c => c.Slug).Distinct().ToList();
        }

        public List<string> GetTags()
        {
            return _dataContext.Content.Select(c => c.Tag).Distinct().ToList();
        }

        public List<string> GetTags(string slug)
        {
            return _dataContext.Content.Where(c => c.Slug == slug).Select(c => c.Tag).Distinct().ToList();
        }

        public List<Content> Get(string slug)
        {
            return _dataContext.Content.Where(p => p.Slug == slug).ToList();
        }

        public Content Get(string slug, string tag)
        {
            return _dataContext.Content.FirstOrDefault(p => p.Slug == slug & p.Tag == tag);
        }

        public void Save(Content content)
        {
            if (content.Id == 0)
                _dataContext.Content.Add(content);

            _dataContext.SaveChanges();
        }

        public Content Get(int id)
        {
            return _dataContext.Content.Find(id);
        }

        public void Delete(Content content)
        {
            _dataContext.Content.Remove(content);
            _dataContext.SaveChanges();
        }
    }
}
