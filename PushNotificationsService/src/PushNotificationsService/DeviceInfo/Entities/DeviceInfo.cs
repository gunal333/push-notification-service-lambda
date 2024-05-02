namespace PushNotificationsService.DeviceInfo.Entities;

public class DeviceInfo
{
    public Guid Id { get; set; }
    public string token { get; set; }
    public string customerId { get; set; }
    
    public string customerDeviceEndpoint { get; set;}
    
}