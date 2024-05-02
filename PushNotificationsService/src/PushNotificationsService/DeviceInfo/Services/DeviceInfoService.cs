using PushNotificationsService.DeviceInfo.Entities;
using PushNotificationsService.DeviceInfo.Repositories;
using PushNotificationsService.Exceptions.CustomExceptions;

namespace PushNotificationsService.DeviceInfo.Services;

public class DeviceInfoService : IDeviceInfoService
{

    private readonly IDeviceInfoRepository _deviceInfoRepository;

    public DeviceInfoService(IDeviceInfoRepository deviceInfoRepository)
    {
        _deviceInfoRepository = deviceInfoRepository;
    }


    public async Task<bool> IsDeviceInfoAlreadyExist(string customerId)
    {
        IEnumerable<Entities.DeviceInfo> existingDeviceInfo = await _deviceInfoRepository.GetDeviceInfoByCustomerId(customerId);
        
        Console.WriteLine("Got the existingDeviceInfo ${0}",customerId);

        return existingDeviceInfo.Any();
    }

    public async Task<Entities.DeviceInfo> SaveCustomerDeviceInfo(Entities.DeviceInfo deviceInfo)
    {

        var savedDeviceInfo = await _deviceInfoRepository.SaveDeviceInfo(deviceInfo);
        
        return savedDeviceInfo;
    }
}