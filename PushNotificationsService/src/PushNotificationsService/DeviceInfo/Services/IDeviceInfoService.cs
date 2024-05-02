using PushNotificationsService.DeviceInfo.Entities;

namespace PushNotificationsService.DeviceInfo.Services;

public interface IDeviceInfoService
{
    Task<bool> IsDeviceInfoAlreadyExist(string customerId);
    
    Task<Entities.DeviceInfo> SaveCustomerDeviceInfo(Entities.DeviceInfo deviceInfo);
    
    
}