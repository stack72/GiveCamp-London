using System;
using System.Linq;
using System.Collections.Generic;

namespace GiveCampStarterKit.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        public TeamRepository(SiteDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private SiteDataContext _dataContext;

        public void Save(Team team)
        {
            if (team.Id == 0)
                _dataContext.Teams.Add(team);

            _dataContext.SaveChanges();
        }

        public Team Get(int id)
        {
            return _dataContext.Teams.Find(id);
        }

        public void Delete(Team team)
        {
            _dataContext.Teams.Remove(team);
            _dataContext.SaveChanges();
        }

        public List<Team> GetAll()
        {
            return (from t in _dataContext.Teams
                        select t).ToList();
        }
    }
}
