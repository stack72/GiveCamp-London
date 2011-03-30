using System;
using System.Collections.Generic;
using System.Linq;

namespace GiveCampLondon.Repositories
{
    public class ExperienceLevelRepository : IExperienceLevelRepository
    {
        public ExperienceLevelRepository(SiteDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private SiteDataContext _dataContext;

        public IList<ExperienceLevel> FindAll()
        {
            return _dataContext.ExperienceLevels.OrderBy(e => e.DisplayOrder).ToList();
        }

        public ExperienceLevel Get(int id)
        {
            return _dataContext.ExperienceLevels.Find(id);
        }

        public ExperienceLevel GetForVolunteerId(int id)
        {
            return (from v in _dataContext.Volunteers where v.Id == id select v.ExperienceLevel).First();
        }
    }
}