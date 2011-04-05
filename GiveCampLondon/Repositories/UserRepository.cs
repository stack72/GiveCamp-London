using Simple.Data;

namespace GiveCampLondon.Repositories
{
    public class UserRepository: IUserRepository
    {
        public Member GetUserByUserName(string userName)
        {
            dynamic db = Database.OpenNamedConnection("SiteDataContext");
            var user = db.User.FindByUserName(userName);

            return user;
        }

        public bool ValidateUser(string userName, string password)
        {
            var validUser = false;
            dynamic db = Database.OpenNamedConnection("SiteDataContext");
            var user = db.User.FindByUserName(userName);
            if (user != null && user.Password == password)
            {
                validUser = true;
            }

            return validUser;
        }
    
        public Member CreateUser(Member newMember)
        {
            dynamic db = Database.OpenNamedConnection("SiteDataContext");
            db.User.Insert(newMember);

            var user = GetUserByUserName(newMember.UserName);
            return user;
        }
    }
}
