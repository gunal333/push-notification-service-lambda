using System.ComponentModel.DataAnnotations;

namespace PushNotificationsService.DeviceInfo.Entities;

public class DeviceInfoRequestDto
{
    [Required]
    public string token { get; set; }
    
    [Required]
    public string customerId { get; set; } 

}