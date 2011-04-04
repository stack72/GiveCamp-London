using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveCampLondon.Repositories
{
    public interface IUserRepository
    {
        dynamic GetUserByUserName(string userName);
        dynamic ValidateUser(string userName, string password);
    }
}
