using System;
using System.Web.Security;

namespace GiveCampStarterKit.Tests
{
	public class TestHelper
	{
		public static MembershipUser CreateUser(string email, string userName)
		{
			return new MembershipUser("AspNetSqlMembershipProvider", userName ?? "Joe", Guid.NewGuid(), email ?? "test@test.test", "", "", true, false,
													 DateTime.Now,
													 DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
		}
	}
}