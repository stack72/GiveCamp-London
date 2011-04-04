using System;
using System.Collections.Generic;

namespace GiveCampLondon.Repositories
{
    public interface IVolunteerRepository
    {
        void Save(Volunteer volunteer);
        Volunteer Get(int id);
        List<Volunteer> GetForTeam(int teamId);
        List<Volunteer> GetAllNotInTeam(int teamId);
        IList<JobRole> FindJobRolesFor(int volunteerId);
		Volunteer Get(Guid membershipId);
        IList<Technology> FindTechnologiesFor(int volunteerId);
        IList<Volunteer> FindAll();
        IList<Volunteer> FindVolunteersForJobRole(int jobRoleId);
        void RemoveAllVolunteersFromJobRole(int jobRoleId);

    }
}
