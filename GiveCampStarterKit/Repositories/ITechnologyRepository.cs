using System.Collections.Generic;

namespace GiveCampStarterKit.Repositories
{
    public interface ITechnologyRepository
    {
        IList<Technology> FindAll();
        Technology Get(int id);
    }
}