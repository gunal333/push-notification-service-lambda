using System.ComponentModel.DataAnnotations;

namespace PushNotificationsService.PushNotificationEndpoint;

public class PushNotificationRequestDto
{
    [Required]
    public string message { get; set; }
    
    [Required]
    public string title { get; set; }
    
    [Required]
    public string customerId { get; set; }
}