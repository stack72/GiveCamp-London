using System.Collections.Generic;

namespace GiveCampLondon.Repositories
{
    public interface ITechnologyRepository
    {
        IList<Technology> FindAll();
        Technology Get(int id);
    }
}