using System.Web.Mvc;

namespace GiveCampLondon.Website.Areas.TeamAdministration
{
    public class TeamAdministrationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "TeamAdministration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "TeamAdministration_GetVolunteersInTeam",
                "teamadministration/{controller}/GetVolunteersInTeam/{teamId}",
                new { action = "GetVolunteersInTeam", });

            context.MapRoute(
                "TeamAdministration_GetVolunteersNotInTeam",
                "teamadministration/{controller}/GetVolunteersNotInTeam/{teamId}",
                new { action = "GetVolunteersNotInTeam", });

            context.MapRoute(
                "TeamAdministration_default",
                "teamadministration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "GiveCampStarterKit.Website.Areas.TeamAdministration.Controllers" }
            );
        }
    }
}
