namespace PushNotificationsService.DeviceInfo.Repositories;

public interface IDeviceInfoRepository
{

    Task<Entities.DeviceInfo> SaveDeviceInfo(Entities.DeviceInfo deviceInfo);

    Task<IEnumerable<Entities.DeviceInfo>> GetDeviceInfoByCustomerId(string customerId);
}