using System;
using System.Web.Security;

namespace GiveCampStarterKit.Services
{
    public class AccountMembershipService : IMembershipService
    {
        private MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
            return currentUser.ChangePassword(oldPassword, newPassword);
        }

        public MembershipUser GetUserByName(string userName)
        {
            MembershipUser currentUser = _provider.GetUser(userName, false);
            return currentUser;
        }

        public MembershipUser GetUserById(Guid id)
        {
            MembershipUser currentUser = _provider.GetUser(id, false);
            return currentUser;
        }

        public void UpdateUser(MembershipUser user)
        {
            _provider.UpdateUser(user);
        }
    }
}