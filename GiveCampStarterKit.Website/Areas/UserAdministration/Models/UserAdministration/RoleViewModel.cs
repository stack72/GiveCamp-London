using System.Collections.Generic;
using System.Web.Security;

namespace GiveCampStarterKit.Website.Areas.UserAdministration.Models.UserAdministration
{
	public class RoleViewModel
	{
		public string Role { get; set; }
		public IEnumerable<MembershipUser> Users { get; set; }
	}
}