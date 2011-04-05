using GiveCampLondon.Repositories;

namespace GiveCampLondon.Website.Models.ServiceFacade
{
    public interface IServiceProxy
    {
        bool ValidateUser(string userName, string password);
        Member GetUserByUserName(string userName);
        Member CreateNewMember(RegisterViewModel newMember);
    }

    public class ServiceProxy : IServiceProxy
    {
        private readonly IUserRepository _userRepository;
        public ServiceProxy(IUserRepository userService)
        {
            _userRepository = userService;
        }

        public bool ValidateUser(string userName, string password)
        {
            return _userRepository.ValidateUser(userName, password);
        }

        public Member GetUserByUserName(string userName)
        {
            return _userRepository.GetUserByUserName(userName);
        }

        public Member CreateNewMember(RegisterViewModel newMember)
        {
            //translate te member type here
            var member = new Member
                             {
                                 FirstName = newMember.FirstName,
                                 LastName = newMember.LastName,
                                 EmailAddress = newMember.EmailAddress,
                                 Password = newMember.Password,
                                 TwitterHandle = newMember.TwitterHandle,
                                 UserName = newMember.UserName
                             };

            return _userRepository.CreateUser(member);
        }
    }
}