using System.Collections.Generic;
using System.Linq;

namespace GiveCampLondon.Repositories
{
    public class ExpertiseRepository : IExpertiseRepository
    {
        public ExpertiseRepository(SiteDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private SiteDataContext _dataContext;

        public void Save(Expertise expertise)
        {
            if (expertise.Id == 0)
                _dataContext.Expertise.Add(expertise);

            _dataContext.SaveChanges();
        }

        public IList<Expertise> FindAll()
        {
            return _dataContext.Expertise.OrderBy(jr => jr.DisplayOrder).ToList();
        }

        public void Delete(Expertise expertise)
        {
            _dataContext.Expertise.Remove(expertise);
            _dataContext.SaveChanges();
        }

        public Expertise Get(int id)
        {
            return _dataContext.Expertise.Find(id);
        }
    }
}