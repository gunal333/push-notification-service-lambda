namespace PushNotificationsService.PushNotificationEndpoint;

public interface IPushNotificationEndpointService
{
    Task<string> CreateDeviceEndpoint(string token);

    Task SendPushNotificationToDevice(string message,string customerId,string title);
}