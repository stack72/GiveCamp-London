using System.Collections.Generic;

namespace GiveCampLondon.Repositories
{
    public interface ISponsorRepository
    {
        void Save(Sponsor volunteer);
        Sponsor Get(int id);
        IList<Sponsor> FindAll();
    }
}
