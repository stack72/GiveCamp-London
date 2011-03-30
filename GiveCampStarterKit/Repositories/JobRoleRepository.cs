using System;
using System.Collections.Generic;
using System.Linq;

namespace GiveCampStarterKit.Repositories
{
    public class JobRoleRepository : IJobRoleRepository
    {
        public JobRoleRepository(SiteDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private SiteDataContext _dataContext;

        public void Save(JobRole jobRole)
        {
            if (jobRole.Id == 0)
                _dataContext.JobRoles.Add(jobRole);

            _dataContext.SaveChanges();
        }

        public IList<JobRole> FindAll()
        {
            return _dataContext.JobRoles.OrderBy(jr => jr.DisplayOrder).ToList();
        }

        public void Delete(JobRole jobRole)
        {
            _dataContext.JobRoles.Remove(jobRole);
            _dataContext.SaveChanges();
        }

        public JobRole Get(int id)
        {
            return _dataContext.JobRoles.Find(id);
        }
    }
}