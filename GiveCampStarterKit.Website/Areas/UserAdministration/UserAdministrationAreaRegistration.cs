﻿using System.Web.Mvc;
using GiveCampStarterKit.Website.Areas.UserAdministration.Controllers;

namespace GiveCampStarterKit.Website.Areas.UserAdministration
{
	public class UserAdministrationAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "UserAdministration";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"UserAdministration_default",
				"UserAdministration/{controller}/{action}/{id}",
				new { area="UserAdministration", action = "Index", id = UrlParameter.Optional },
				new [] { typeof(UserAdministrationController).Namespace }
			);
		}
	}
}
