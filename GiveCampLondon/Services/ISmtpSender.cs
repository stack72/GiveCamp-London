using System.Net.Mail;

namespace GiveCampLondon.Services
{
	public interface ISmtpSender
	{
		bool Send(MailMessage message);
	}
}