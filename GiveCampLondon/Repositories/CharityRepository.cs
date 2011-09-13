using System;
using System.Collections.Generic;
using System.Linq;

namespace GiveCampLondon.Repositories
{
    public class CharityRepository : ICharityRepository
    {
        public CharityRepository(SiteDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private readonly SiteDataContext _dataContext;

        public void Save(Charity charity)
        {
            if (charity.Id == 0)
                _dataContext.Charities.Add(charity);

            _dataContext.SaveChanges();
        }

        public Charity Get(int id)
        {
            return _dataContext.Charities.Find(id);
        }

        public Charity Get(Guid membershipId)
        {
            return _dataContext.Charities.FirstOrDefault(c => c.MembershipId == membershipId);
        }

        public void Delete(Charity charity)
        {
            _dataContext.Charities.Remove(charity);
            _dataContext.SaveChanges();
        }

        public IList<Charity> GetSupportedCharities()
        {
            return _dataContext.Charities.Where(x => x.Approved).ToList();
        }
    }
}
