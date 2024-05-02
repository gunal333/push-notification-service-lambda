namespace PushNotificationsService.Exceptions.CustomExceptions;

public class DuplicateDeviceInfoException : Exception
{
    public DuplicateDeviceInfoException(string message) : base(message) { }
}