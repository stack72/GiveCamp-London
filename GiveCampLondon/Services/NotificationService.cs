using System;
using System.Collections;
using System.Net.Mail;
using System.Web.Security;
using Antlr3.ST;
using GiveCampLondon.Repositories;

namespace GiveCampLondon.Services
{
	public class NotificationService : INotificationService
	{
		private IContentRepository _contentRepository;
		private ISmtpSender _sender;
		private MailConfiguration _mailConfiguration;
        private readonly IMembershipService _membershipProvider;
		  

	    public NotificationService(IContentRepository contentRepository, ISmtpSender sender, MailConfiguration mailConfiguration, IMembershipService membershipService)
		{
			_contentRepository = contentRepository;
			_sender = sender;
			_mailConfiguration = mailConfiguration;
            _membershipProvider = membershipService;
		}

		public class EmailContext
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string SiteName { get; set; }
			public string UserName { get; set; }
			public string Name { get; set; }
		}

		public bool SendVolunteerNotification(Volunteer user, VolunteerNotificationTemplate volunteerNotificationType)
		{
		    var membershipUser = _membershipProvider.GetUserById(user.MembershipId);

			var context = new EmailContext
			              	{
			              		FirstName = user.FirstName,
								LastName = user.LastName,
								SiteName = _mailConfiguration.SiteName,
								UserName = membershipUser.UserName
			              	};

			var message = new MailMessage(_mailConfiguration.SiteEmailAddress, membershipUser.Email)
							{
								Subject = GetTemplate(volunteerNotificationType.ToString().ToLower() + "-subject", context),
								Body = GetTemplate(volunteerNotificationType.ToString().ToLower() + "-body", context),
								IsBodyHtml = true
							};

			return _sender.Send(message);
		}

		public bool SendCharityNotification(Charity charity, CharityNotificationTemplate charityNotificationType)
		{
			var membershipUser = _membershipProvider.GetUserById(charity.MembershipId);

			var context = new EmailContext
			{
				Name = charity.Name,
				SiteName = _mailConfiguration.SiteName,
				UserName = membershipUser.UserName
			};

			var message = new MailMessage(_mailConfiguration.SiteEmailAddress, membershipUser.Email)
			{
				Subject = GetTemplate(charityNotificationType.ToString().ToLower() + "-subject", context),
				Body = GetTemplate(charityNotificationType.ToString().ToLower() + "-body", context),
				IsBodyHtml = true
			};

			return _sender.Send(message);
		}

		private string GetTemplate(string templateSlug, EmailContext context)
		{
			var template = new StringTemplate(_contentRepository.Get(templateSlug, "email-template").ContentText);
			template.SetAttribute("FirstName", context.FirstName ?? "");
			template.SetAttribute("LastName ", context.LastName ?? "");
			template.SetAttribute("Name", context.Name ?? "");
			template.SetAttribute("SiteName", context.SiteName ?? "");
			template.SetAttribute("UserName", context.UserName ?? "");
			return template.ToString();
		}
	}

	public enum VolunteerNotificationTemplate
	{
		WelcomeVolunteer
	}

	public enum CharityNotificationTemplate
	{
		WelcomeCharity
	}
}