using System.Collections.Generic;

namespace GiveCampLondon.Repositories
{
    public interface IContentRepository
    {

        List<string> GetSlugs();
        List<string> GetTags();
        List<string> GetTags(string slug);
        List<Content> Get(string slug);
        Content Get(string slug, string tag);
        void Save(Content content);
    }
}
