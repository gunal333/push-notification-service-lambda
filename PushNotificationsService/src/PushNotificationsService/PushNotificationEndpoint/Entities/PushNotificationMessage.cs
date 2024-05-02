namespace PushNotificationsService.PushNotificationEndpoint;

public class PushNotificationMessage
{
 public Gcm GCM;
}

public class Gcm
{
 public Notification notification;
}

public class Notification
{
 public string body;
 public string title;
}
