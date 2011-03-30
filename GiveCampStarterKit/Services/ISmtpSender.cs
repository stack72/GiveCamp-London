using System.Net.Mail;

namespace GiveCampStarterKit.Services
{
	public interface ISmtpSender
	{
		bool Send(MailMessage message);
	}
}