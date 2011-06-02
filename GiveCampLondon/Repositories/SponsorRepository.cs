using System.Collections.Generic;
using System.Linq;

namespace GiveCampLondon.Repositories
{
    public class SponsorRepository : ISponsorRepository
    {
        public SponsorRepository(SiteDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private SiteDataContext _dataContext;

        public void Save(Sponsor sponsor)
        {
            if (sponsor.Id == 0)
            {
                _dataContext.Sponsors.Add(sponsor);
                _dataContext.SaveChanges();             
            }

            _dataContext.SaveChanges();
        }

        public Sponsor Get(int id)
        {
            var sponsor = _dataContext.Sponsors.Find(id);
            return sponsor;
        }

        public IList<Sponsor> FindAll()
        {
            return _dataContext.Sponsors.ToList();
        }
    }
}