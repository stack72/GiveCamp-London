using System;
using System.Collections.Generic;
using System.Linq;

namespace GiveCampStarterKit.Repositories
{
    public class TechnologyRepository : ITechnologyRepository
    {
        public TechnologyRepository(SiteDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private SiteDataContext _dataContext;

        public IList<Technology> FindAll()
        {
            return _dataContext.Technologies.OrderBy(t => t.DisplayOrder).ToList();
        }

        public Technology Get(int id)
        {
            return _dataContext.Technologies.Find(id);
        }
    }
}