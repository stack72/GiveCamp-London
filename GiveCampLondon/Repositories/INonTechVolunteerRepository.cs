using System;
using System.Collections.Generic;

namespace GiveCampLondon.Repositories
{
    public interface INonTechVolunteerRepository
    {
        void Save(NonTechVolunteer volunteer);
        NonTechVolunteer Get(int id);
        IList<Expertise> FindExpertiseFor(int volunteerId);
        NonTechVolunteer Get(Guid membershipId);
        IList<NonTechVolunteer> FindAll();
        IList<NonTechVolunteer> FindVolunteersForExpertiseRole(int expertiseId);
    }
}
