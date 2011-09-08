using System;
using System.Collections.Generic;
using System.Linq;

namespace GiveCampLondon.Repositories
{
    public class VolunteerRepository : IVolunteerRepository
    {
        public VolunteerRepository(SiteDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private SiteDataContext _dataContext;

        public void Save(Volunteer volunteer)
        {
            if (volunteer.Id == 0)
            {
                _dataContext.Volunteers.Add(volunteer);
                _dataContext.SaveChanges();             
            }

            if (volunteer.Id != 0 && volunteer.JobRoles != null && volunteer.JobRoles.Count > 0)
            {
                foreach (var volunteerJobRole in _dataContext.VolunteerJobRoles.Where(vjr => vjr.VolunteerId == volunteer.Id))
                {
                    if (!volunteer.JobRoles.Any(jr => jr.Id == volunteerJobRole.JobRoleId))
                        _dataContext.VolunteerJobRoles.Remove(volunteerJobRole);
                }
                _dataContext.SaveChanges();

                foreach (var jobRole in volunteer.JobRoles.Where(jr => !_dataContext.VolunteerJobRoles.Any(vjr => vjr.JobRoleId == jr.Id)))
                {
                    _dataContext.VolunteerJobRoles.Add(new VolunteerJobRole()
                    {
                        VolunteerId = volunteer.Id,
                        JobRoleId = jobRole.Id
                    });
                }
                _dataContext.SaveChanges();
            }

            if (volunteer.Id != 0 && volunteer.Technologies != null && volunteer.Technologies.Count > 0)
            {
                foreach (var volunteerTechnology in _dataContext.VolunteerTechnologies.Where(vt => vt.VolunteerId == volunteer.Id))
                {
                    if (!volunteer.Technologies.Any(t => t.Id == volunteerTechnology.TechnologyId))
                        _dataContext.VolunteerTechnologies.Remove(volunteerTechnology);
                }
                _dataContext.SaveChanges();

                foreach (var technology in volunteer.Technologies.Where(t => !_dataContext.VolunteerTechnologies.Any(vt => vt.TechnologyId == t.Id)))
                {
                    _dataContext.VolunteerTechnologies.Add(new VolunteerTechnology()
                    {
                        VolunteerId = volunteer.Id,
                        TechnologyId = technology.Id
                    });
                }
                _dataContext.SaveChanges();
            }

            _dataContext.SaveChanges();
        }

        public Volunteer Get(int id)
        {
            var volunteer = _dataContext.Volunteers.Find(id);
            return volunteer;
        }

        public Volunteer Get(Guid membershipId)
        {
            return _dataContext.Volunteers.FirstOrDefault(c => c.MembershipId == membershipId);
        }

        public void Delete(Volunteer volunteer)
        {
            foreach (var volunteerJobRole in _dataContext.VolunteerJobRoles.Where(vjr => vjr.VolunteerId == volunteer.Id))
            {
                _dataContext.VolunteerJobRoles.Remove(volunteerJobRole);
            }
            foreach (var volunteerTechnology in _dataContext.VolunteerTechnologies.Where(vt => vt.VolunteerId == volunteer.Id))
            {
                _dataContext.VolunteerTechnologies.Remove(volunteerTechnology);
            }
            _dataContext.Volunteers.Remove(volunteer);
            _dataContext.SaveChanges();
        }

        public void CancelRegistration(int VolunteerId)
        {
            var volunteer = _dataContext.Volunteers.Where(x => x.Id == VolunteerId).FirstOrDefault();
            volunteer.HasCancelled = true;
            _dataContext.SaveChanges();
        }

        public IList<Volunteer> FindVolunteersForJobRole(int jobRoleId)
        {
            var volunteers= (from jr in _dataContext.JobRoles
             join vjr in _dataContext.VolunteerJobRoles on jr.Id equals vjr.JobRoleId
             join v in _dataContext.Volunteers on vjr.VolunteerId equals v.Id
             where vjr.JobRoleId == jobRoleId
             select v).ToList();

            return volunteers;
        }

        public void RemoveAllVolunteersFromJobRole(int jobRoleId)
        {
            var vjr = _dataContext.VolunteerJobRoles.Where(vj => vj.JobRoleId == jobRoleId);
            foreach (var volunteerJobRole in vjr)
            {
                _dataContext.VolunteerJobRoles.Remove(volunteerJobRole);
            }
        }

        public IList<JobRole> FindJobRolesFor(int volunteerId)
        {
            return (from jr in _dataContext.JobRoles
                    join vjr in _dataContext.VolunteerJobRoles on jr.Id equals vjr.JobRoleId
                    join v in _dataContext.Volunteers on vjr.VolunteerId equals v.Id
                    where vjr.VolunteerId == volunteerId
                    select jr).ToList();
        }

        public IList<Technology> FindTechnologiesFor(int volunteerId)
        {
            return (from t in _dataContext.Technologies
                    join vt in _dataContext.VolunteerTechnologies on t.Id equals vt.TechnologyId
                    join v in _dataContext.Volunteers on vt.VolunteerId equals v.Id
                    where vt.VolunteerId == volunteerId
                    select t).ToList();
        }

        public IList<Volunteer> FindAll()
        {
            return _dataContext.Volunteers.ToList();
        }

        public List<Volunteer> GetForTeam(int teamId)
        {
            return (from v in _dataContext.Volunteers
                    where v.TeamId == teamId
                    select v).ToList();
        }

        public List<Volunteer> GetAllNotInTeam(int teamId)
        {
            return (from v in _dataContext.Volunteers
                    where v.TeamId != teamId || v.TeamId == null
                    select v).ToList();
        }
    }
}