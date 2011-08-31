namespace GiveCampLondon.Services
{
	public interface INotificationService
	{
	    bool SendNotification(string emailAddress, VolunteerNotificationTemplate volunteerNotificationType);
	    bool SendContactUsMail(string email, string userEmail);
	}
}