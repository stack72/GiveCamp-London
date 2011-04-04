using System.Configuration;
using System.Net.Mail;
using System.Web.Security;
using GiveCampLondon.Repositories;
using GiveCampLondon.Services;
using GiveCampLondon.Website.Models;
using MvcMembership;
using MvcMembership.Settings;
using StructureMap.Configuration.DSL;

namespace GiveCampLondon.Website.Configuration
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
            });
            For<IFormsAuthentication>().Use<FormsAuthenticationService>();
            For<IUserRepository>().Use<UserRepository>();
            For<MembershipProvider>().Use(Membership.Provider);
            For<IMembershipSettings>().Use(() => new AspNetMembershipProviderSettingsWrapper(Membership.Provider));
            For<IUserService>().Use<AspNetMembershipProviderWrapper>();
            For<IPasswordService>().Use<AspNetMembershipProviderWrapper>();
            For<IRolesService>().Use<AspNetRoleProviderWrapper>();
            For<RoleProvider>().Use(Roles.Provider);
            For<ISmtpClient>().Use(() => new SmtpClientProxy(new SmtpClient()));
        	For<MailConfiguration>().Use(() => new MailConfiguration
        	                                   	{
        	                                   		SiteEmailAddress =
        	                                   			ConfigurationManager.AppSettings["SiteEmailAddress"] ?? "",
        	                                   		SiteName = ConfigurationManager.AppSettings["SiteName"] ?? ""
        	                                   	});

        }
    }
}