﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Exceptioneer.WebClient;
using GiveCampLondon.Configuration;
using GiveCampLondon.Website.Configuration;
using StructureMap;

namespace GiveCampLondon.Website
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.([iI][cC][oO]|[gG][iI][fF])(/.*)?" });

            routes.MapRoute(
                null, // Route name
                "about-givecamp", // URL with parameters
                new { controller = "Home", action = "About-GiveCamp" },
                new[] { "GiveCampLondon.Website.Controllers" }
            );

            routes.MapRoute(
                null, // Route name
                "schedule", // URL with parameters
                new { controller = "Home", action = "Schedule" },
                new[] { "GiveCampLondon.Website.Controllers" }
            );

            routes.MapRoute(
                null, // Route name
                "contact-us", // URL with parameters
                new { controller = "Home", action = "Contact-Us" },
                new[] { "GiveCampLondon.Website.Controllers" }
            );

            routes.MapRoute(
                null, // Route name
                "faq/{id}", // URL with parameters
                new { controller = "FAQ", action = "FAQ", id = UrlParameter.Optional },
                new[] { "GiveCampLondon.Website.Controllers" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "GiveCampLondon.Website.Controllers" }
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

        protected void Application_Error(Object sender, EventArgs e)
        {
            var error = Server.GetLastError();
            try
            {
                var wc = new Client()
                                 {
                                     ApiKey = "C98ADB4D-3100-4DA7-9100-4A6693D1D3CB",
                                     ApplicationName = "GiveCamp UK",
                                     CurrentException = error
                                 };

                wc.Submit();
            }
            catch (Exception)
            {
                //this is handled quietly as we dont want exceptioneer errors affecting the site
            }

            Response.Redirect("~/Error");
        }
    }
}