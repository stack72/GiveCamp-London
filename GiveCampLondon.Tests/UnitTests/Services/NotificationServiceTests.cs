using System;
using System.Net.Mail;
using System.Web.Security;
using GiveCampLondon.Repositories;
using GiveCampLondon.Services;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap.AutoMocking;

namespace GiveCampLondon.Tests.UnitTests.Services
{
	[TestFixture]
	public class NotificationServiceTests
	{
        private RhinoAutoMocker<NotificationService> _service;
		private MembershipUser _membershipUser;

	    [SetUp]
		public void Setup()
		{
			_membershipUser = TestHelper.CreateUser(null, null);
			_service = new RhinoAutoMocker<NotificationService>();
			var mainConfig = _service.Get<MailConfiguration>();
			mainConfig.SiteEmailAddress = "admin@giveCampSite.com";
			mainConfig.SiteName = "Dallas Give Camp";
		}

		[Test]
		public void Will_Send_User_Notification_For_Template()
		{
			var user = new Volunteer {FirstName = "John", LastName = "Doe"};
			_service.Get<IMembershipService>().Stub(s => s.GetUserById(Guid.NewGuid())).IgnoreArguments().Return(_membershipUser);
			_service.Get<IContentRepository>().Stub(s => s.Get("", "")).IgnoreArguments().Return(new Content()
			{
				ContentText =
					"$FirstName$ Welcome Subject"
			}).Repeat.Once();

			_service.Get<IContentRepository>().Stub(s => s.Get("", "")).IgnoreArguments().Return(new Content()
			{
				ContentText =
					"$FirstName$ Welcome Body"
			}).Repeat.Once();

            _service.Get<ISmtpSender>()
				.Expect(s => s.Send(Arg<MailMessage>.Matches(m => m.From.Address == "admin@giveCampSite.com"
					&& m.To[0].Address == _membershipUser.Email && m.Body == "John Welcome Body" && m.Subject == "John Welcome Subject"))).Return(true);

            _service.Get<IContentRepository>()
                .Stub(c => c.Get("welcomevolunteer-subject", "email-template"))
                .Return(new Content { ContentText = "teh subject" }); ;

            _service.Get<IContentRepository>()
                .Stub(c => c.Get("welcomevolunteer-body", "email-template"))
                .Return(new Content { ContentText = "teh body" }); ;

			_service.ClassUnderTest.SendVolunteerNotification(user, VolunteerNotificationTemplate.WelcomeVolunteer);
            _service.Get<ISmtpSender>().VerifyAllExpectations();
		}

		[Test, Explicit("Load Test")]
		public void Load_Test_For_Razor_Template_Engine()
		{
			var user = new Volunteer { FirstName = "John", LastName = "Doe" };
			_service.Get<IMembershipService>().Stub(s => s.GetUserById(Guid.NewGuid())).IgnoreArguments().Return(_membershipUser);
			_service.Get<IContentRepository>().Stub(s => s.Get("", "")).IgnoreArguments().Return(new Content()
			{
				ContentText =
					"$FirstName$ Welcome Subject"
			});
			_service.Get<ISmtpSender>()
				.Expect(s => s.Send(Arg<MailMessage>.Matches(m => m.From.Address == "admin@giveCampSite.com"
					&& m.To[0].Address == _membershipUser.Email))).Return(true);

			for (int i = 0; i < 10000; i++)
			{
				_service.ClassUnderTest.SendVolunteerNotification(user, VolunteerNotificationTemplate.WelcomeVolunteer);
				System.Diagnostics.Trace.WriteLine(i);
			}
		}
	}
}