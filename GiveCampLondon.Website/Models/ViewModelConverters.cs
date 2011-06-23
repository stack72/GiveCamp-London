using GiveCampLondon;
using GiveCampLondon.Website.Models.Volunteer;

namespace GiveCampLondon.Website.Models
{
    public static class ViewModelConverters
    {
        public static GiveCampLondon.Charity MapToCharityModel(this Charity.SignUpViewModel vm)
        {
            var charity = new GiveCampLondon.Charity
            {
                CharityName = vm.Name,
                BackgroundInformation = vm.BackgroundInformation,
                OtherInfrastructure = vm.OtherInfrastructure,
                OtherSupportSkills = vm.OtherSupportSkills,
                WorkRequested = vm.WorkRequested,
                Email = vm.Email,
                Website = vm.Website,
                ContactName = vm.ContactName,
                ContactPhone = vm.ContactPhone,
                Approved = false
            };

            return charity;

        }

        public static GiveCampLondon.Volunteer MapToVolunteerModel(this Volunteer.SignUpViewModel model)
        {
            var volunteer = new GiveCampLondon.Volunteer
                                {
                                    FirstName = model.FirstName,
                                    LastName = model.LastName,
                                    Bio = model.Bio,
                                    Comments = model.Comments,
                                    DietaryNeeds = model.DietaryNeeds,
                                    Email = model.Email,
                                    HasExtraLaptop = model.HasExtraLaptop,
                                    HasLaptop = model.HasLaptop,
                                    IsGoodGuiDesigner = model.IsGoodGuiDesigner,
                                    IsStudent = model.IsStudent,
                                    JobDescription = model.JobDescription,
                                    PhoneNumber = model.PhoneNumber,
                                    ShirtSize = model.ShirtSize,
                                    ShirtStyle = model.ShirtStyle,
                                    TeamName = model.TeamName,
                                    TwitterHandle = model.TwitterHandle,
                                    YearsOfExperience = model.YearsOfExperience
                                };
            return volunteer;
        }

        public static NonTechVolunteer MapToNonTechVolunteerModel(this NonTechVolunteerViewModel model)
        {
            var volunteer = new NonTechVolunteer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                JobDescription = model.JobDescription,
                DietaryNeeds = model.DietaryNeeds,
                TwitterHandle = model.TwitterHandle,
                Bio = model.Bio,
                ShirtSize = model.ShirtSize,
                ShirtStyle = model.ShirtStyle,
                SkillSet = model.SkillsOutline,
                SessionDetails = model.ExpertiseTopic

            };
            return volunteer;
        }
    }
}