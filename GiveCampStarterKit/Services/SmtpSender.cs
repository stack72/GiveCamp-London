using System;
using System.Net.Mail;

namespace GiveCampStarterKit.Services
{
	public class SmtpSender : ISmtpSender
	{
		private MailConfiguration _mailConfiguration;

		public SmtpSender(MailConfiguration mailConfiguration)
		{
			_mailConfiguration = mailConfiguration;
		}

		public bool Send(MailMessage message)
		{
			var client = new SmtpClient();
			try
			{
				client.Send(message);
			}
			catch (Exception ex)
			{
				//log exception!
				return false;
			}
			
			return true;
		}
	}
}