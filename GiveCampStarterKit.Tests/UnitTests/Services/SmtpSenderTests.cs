using System.Net.Mail;
using GiveCampStarterKit.Services;
using NUnit.Framework;
using StructureMap.AutoMocking;

namespace GiveCampStarterKit.Tests.UnitTests.Services
{
	[TestFixture]
	public class SmtpSenderTests
	{
		private SmtpSender _service;

		[SetUp]
		public void SetUp()
		{
			_service = new SmtpSender(new MailConfiguration { SiteEmailAddress = "test@givecamp.com" });
		}

		[Test, Explicit] //This is integration test
		public void Can_Send_SmtpEmail()
		{
			var message = new MailMessage("test@givecamp.com", "test@givecampuser.com", "test subject", "test body");
			_service.Send(message);
		}
	}
}