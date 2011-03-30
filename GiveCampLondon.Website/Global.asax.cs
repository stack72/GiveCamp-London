using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using GiveCampLondon.Website.Configuration;
using StructureMap;
using GiveCampLondon.Configuration;

namespace GiveCampLondon.Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "GiveCampStarterKit.Website.Controllers" }
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<StructureMapRegistry>();
                x.AddRegistry<WebRegistry>();
            });

        }

        protected void Application_AuthenticateRequest()
        {
            if (HttpContext.Current.User != null)
                Membership.GetUser(true);
        }
    }
}