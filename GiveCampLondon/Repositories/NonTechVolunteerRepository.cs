using System;
using System.Collections.Generic;
using System.Linq;

namespace GiveCampLondon.Repositories
{
    public class NonTechVolunteerRepository : INonTechVolunteerRepository
    {
        public NonTechVolunteerRepository(SiteDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private SiteDataContext _dataContext;

        public void Save(NonTechVolunteer volunteer)
        {
            if (volunteer.Id == 0)
            {
                _dataContext.NonTechVolunteers.Add(volunteer);
                _dataContext.SaveChanges();             
            }

            if (volunteer.Id != 0 && volunteer.AreasOfExpertise != null && volunteer.AreasOfExpertise.Count > 0)
            {
                foreach (var volunteerExpertise in _dataContext.NonTechVolunteerExpertise.Where(vjr => vjr.VolunteerId == volunteer.Id))
                {
                    if (!volunteer.AreasOfExpertise.Any(jr => jr.Id == volunteerExpertise.ExpertiseId))
                        _dataContext.NonTechVolunteerExpertise.Remove(volunteerExpertise);
                }
                _dataContext.SaveChanges();

                foreach (var expertise in volunteer.AreasOfExpertise.Where(jr => !_dataContext.NonTechVolunteerExpertise.Any(vjr => vjr.ExpertiseId == jr.Id)))
                {
                    _dataContext.NonTechVolunteerExpertise.Add(new NonTechVolunteerExpertise()
                    {
                        VolunteerId = volunteer.Id,
                        ExpertiseId = expertise.Id
                    });
                }
                _dataContext.SaveChanges();
            }

            _dataContext.SaveChanges();
        }

        public NonTechVolunteer Get(int id)
        {
            var volunteer = _dataContext.NonTechVolunteers.Find(id);
            return volunteer;
        }

        public IList<NonTechVolunteer> FindVolunteersForExpertiseRole(int expertiseId)
        {
            var volunteers= (from jr in _dataContext.Expertise
             join vjr in _dataContext.NonTechVolunteerExpertise on jr.Id equals vjr.ExpertiseId
             join v in _dataContext.NonTechVolunteers on vjr.VolunteerId equals v.Id
                             where vjr.ExpertiseId == expertiseId
             select v).ToList();

            return volunteers;
        }

        public IList<Expertise> FindExpertiseFor(int volunteerId)
        {
            return (from jr in _dataContext.Expertise
                    join vjr in _dataContext.NonTechVolunteerExpertise on jr.Id equals vjr.ExpertiseId
                    join v in _dataContext.Volunteers on vjr.VolunteerId equals v.Id
                    where vjr.VolunteerId == volunteerId
                    select jr).ToList();
        }

        public IList<NonTechVolunteer> FindAll()
        {
            return _dataContext.NonTechVolunteers.ToList();
        }

        public NonTechVolunteer Get(Guid membershipId)
        {
            return _dataContext.NonTechVolunteers.FirstOrDefault(c => c.MembershipId == membershipId);
        }

    }
}