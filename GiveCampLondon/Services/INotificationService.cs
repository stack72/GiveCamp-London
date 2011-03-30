namespace GiveCampLondon.Services
{
	public interface INotificationService
	{
		bool SendVolunteerNotification(Volunteer user, VolunteerNotificationTemplate volunteerNotificationType);
		bool SendCharityNotification(Charity charity, CharityNotificationTemplate charityNotificationType);
	}
}