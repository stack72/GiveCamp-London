using System.Collections.Generic;

namespace GiveCampStarterKit.Repositories
{
    public interface IJobRoleRepository
    {
        void Save(JobRole jobRole);
        IList<JobRole> FindAll();
        void Delete(JobRole jobRole);
        JobRole Get(int id);
    }
}