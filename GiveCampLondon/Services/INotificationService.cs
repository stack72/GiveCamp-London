namespace GiveCampLondon.Services
{
	public interface INotificationService
	{
	    bool SendNotification(string emailAddress, VolunteerNotificationTemplate volunteerNotificationType);
	}
}