using System;
using System.Net.Mail;

namespace GiveCampLondon.Services
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
			var client = new SmtpClient {EnableSsl = true};
		    try
			{
				client.Send(message);
			}
			catch
			{
				//log exception!
				return false;
			}
			
			return true;
		}
	}
}