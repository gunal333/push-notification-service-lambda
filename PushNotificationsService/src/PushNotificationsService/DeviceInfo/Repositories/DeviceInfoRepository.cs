using System.Data;
using Microsoft.EntityFrameworkCore;
using PushNotificationsService.DbContextConfig;

namespace PushNotificationsService.DeviceInfo.Repositories;

public class DeviceInfoRepository : IDeviceInfoRepository
{
    private readonly PushNotificationContext _notificationContext;
    
    public DeviceInfoRepository(PushNotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }
    
    public async Task<Entities.DeviceInfo> SaveDeviceInfo(Entities.DeviceInfo deviceInfo )
    {
        Entities.DeviceInfo savedDeviceInfo = null;
        try {
            var strategy = _notificationContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () => {
                await using var transaction =
                    await _notificationContext.Database.BeginTransactionAsync(IsolationLevel.Serializable);

                 savedDeviceInfo =(await _notificationContext.DeviceInfos.AddAsync(deviceInfo)).Entity;
                await _notificationContext.SaveChangesAsync();
                await transaction.CommitAsync();
            });
            return savedDeviceInfo;
        }
        catch (Exception ex) {
            Console.WriteLine("Exception in saving the device info ${0}",ex);
            throw;
        }
    }
    
    public async Task< IEnumerable<Entities.DeviceInfo>> GetDeviceInfoByCustomerId(string customerId)
    {
        return  await _notificationContext.DeviceInfos.Where(di => di.customerId == customerId).ToListAsync();
    }
}