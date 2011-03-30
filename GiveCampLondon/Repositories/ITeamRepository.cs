using System;
using System.Collections.Generic;

namespace GiveCampLondon.Repositories
{
    public interface ITeamRepository
    {
        void Save(Team team);
        Team Get(int id);
        void Delete(Team team);
        List<Team> GetAll();
    }
}
