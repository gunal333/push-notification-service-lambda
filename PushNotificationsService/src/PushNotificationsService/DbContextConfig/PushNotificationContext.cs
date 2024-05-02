using Microsoft.EntityFrameworkCore;
using PushNotificationsService.DeviceInfo.Entities;


namespace PushNotificationsService.DbContextConfig;

public class PushNotificationContext : DbContext
{
    public DbSet<DeviceInfo.Entities.DeviceInfo> DeviceInfos { get; set; }


    public PushNotificationContext(DbContextOptions<PushNotificationContext> options)

        : base(options)

    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)

    {

        modelBuilder.Entity<DeviceInfo.Entities.DeviceInfo>();

    }

}