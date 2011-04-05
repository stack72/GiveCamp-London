namespace GiveCampLondon.Repositories
{
    public interface IUserRepository
    {
        Member GetUserByUserName(string userName);
        bool ValidateUser(string userName, string password);
        Member CreateUser(Member newMember);
    }
}
