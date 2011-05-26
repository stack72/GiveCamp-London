using System.Net.Mail;
using Antlr3.ST;
using GiveCampLondon.Repositories;

namespace GiveCampLondon.Services
{
	public class NotificationService : INotificationService
	{
		private readonly IContentRepository _contentRepository;
		private readonly ISmtpSender _sender;
		private readonly MailConfiguration _mailConfiguration;

	    public NotificationService(IContentRepository contentRepository, ISmtpSender sender, MailConfiguration mailConfiguration)
		{
			_contentRepository = contentRepository;
			_sender = sender;
			_mailConfiguration = mailConfiguration;
		}


        public bool SendNotification(string email, VolunteerNotificationTemplate volunteerNotificationType)
		{
            var message = new MailMessage(_mailConfiguration.SiteEmailAddress, email)
            {
                Subject = GetTemplate(volunteerNotificationType.ToString().ToLower() + "-subject"),
                Body = GetTemplate(volunteerNotificationType.ToString().ToLower() + "-body"),
                IsBodyHtml = true
            };

            return _sender.Send(message);
		}

	    private string GetTemplate(string templateSlug)
		{
			var template = new StringTemplate(_contentRepository.Get(templateSlug, "email-template").ContentText);
			return template.ToString();
		}
	}

	public enum VolunteerNotificationTemplate
	{
		WelcomeVolunteer
	}
}