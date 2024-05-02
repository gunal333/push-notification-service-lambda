using System.Text.Json;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Newtonsoft.Json;
using PushNotificationsService.DeviceInfo.Repositories;

namespace PushNotificationsService.PushNotificationEndpoint;

public class PushNotificationEndpointService : IPushNotificationEndpointService
{
    private IAmazonSimpleNotificationService _snsClient;
    private IConfiguration _configuration;
    private IDeviceInfoRepository _deviceInfoRepository;
    
    public PushNotificationEndpointService(IAmazonSimpleNotificationService snsClient,IConfiguration configuration,IDeviceInfoRepository deviceInfoRepository)
    {
        _snsClient = snsClient;
        _configuration = configuration;
        _deviceInfoRepository = deviceInfoRepository;
    }

    public async Task<string> CreateDeviceEndpoint(string token)
    {
        try
        {
            var endpointRequest = new CreatePlatformEndpointRequest
            {
                PlatformApplicationArn = _configuration["SNSNotificationArn"],
                Token = token
            };
            Console.WriteLine("Publishing message");
            var createEndpointResponse = await _snsClient.CreatePlatformEndpointAsync(endpointRequest);

            Console.WriteLine("The endpoint ${0} : ",createEndpointResponse.EndpointArn);
            return createEndpointResponse.EndpointArn;
            

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SendPushNotificationToDevice(string message,string customerId,string title)
    {
        
        var deviceInfo =  await _deviceInfoRepository.GetDeviceInfoByCustomerId(customerId);

        var GCM = new Gcm()
        {
            notification = new Notification()
            {
                body = message,
                title = title
            }
        };

        var notification = JsonConvert.SerializeObject(GCM);

        notification = notification.Replace("\"","\\\"");
        
        var snsMessage = "{\"GCM\": \"" + notification +"\"}";
        
        var request = new PublishRequest
        {
            MessageStructure = "json",
            TargetArn = deviceInfo.First()
                .customerDeviceEndpoint,
            Message = snsMessage
        };

        var publishResponse = await _snsClient.PublishAsync(request);
        Console.WriteLine("the response is {0} {1} {2}",publishResponse.HttpStatusCode, publishResponse.MessageId, snsMessage);
    }
}