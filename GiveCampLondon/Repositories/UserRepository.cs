using Simple.Data;

namespace GiveCampLondon.Repositories
{
    public class UserRepository: IUserRepository
    {
        public dynamic GetUserByUserName(string userName)
        {
            dynamic db = Database.Open();
            var user = db.User.FindByUserName(userName);

            return user;
        }

        public dynamic ValidateUser(string userName, string password)
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
    }
}
