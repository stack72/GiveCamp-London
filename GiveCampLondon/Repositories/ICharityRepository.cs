using System;

namespace GiveCampLondon.Repositories
{
    public interface ICharityRepository
    {
        void Save(Charity charity);
        Charity Get(int id);
        Charity Get(Guid membershipId);
        void Delete(Charity updatedCharity);
    }
}
