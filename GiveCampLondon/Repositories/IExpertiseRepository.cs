using System.Collections.Generic;

namespace GiveCampLondon.Repositories
{
    public interface IExpertiseRepository
    {
        void Save(Expertise expertise);
        IList<Expertise> FindAll();
        void Delete(Expertise expertise);
        Expertise Get(int id);
    }
}