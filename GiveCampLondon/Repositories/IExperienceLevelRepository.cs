using System.Collections.Generic;

namespace GiveCampLondon.Repositories
{
    public interface IExperienceLevelRepository
    {
        IList<ExperienceLevel> FindAll();
        ExperienceLevel Get(int id);
        ExperienceLevel GetForVolunteerId(int id);
    }
}