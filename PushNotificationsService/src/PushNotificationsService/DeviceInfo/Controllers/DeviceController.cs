using System.Net;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Mvc;
using PushNotificationsService.DeviceInfo.Entities;
using PushNotificationsService.DeviceInfo.Repositories;
using PushNotificationsService.DeviceInfo.Services;
using PushNotificationsService.Exceptions.CustomExceptions;
using PushNotificationsService.PushNotificationEndpoint;

namespace PushNotificationsService.Controllers;

[Route("/device-info")]
public class DeviceInfoController : ControllerBase
{
    private IDeviceInfoService _deviceInfoService;
    private IPushNotificationEndpointService _pushNotificationEndpointService;

    public DeviceInfoController( IDeviceInfoService deviceInfoService, IPushNotificationEndpointService pushNotificationEndpointService)
    {
        _deviceInfoService = deviceInfoService;
        _pushNotificationEndpointService = pushNotificationEndpointService;
    }

    [HttpPost]
    public async Task<JsonResult> CreateNotificationArnAndSaveDeviceInfo([FromBody] DeviceInfoRequestDto request,
        CancellationToken cancellationToken)
    {
        bool isAlreadyExist = await _deviceInfoService.IsDeviceInfoAlreadyExist(request.customerId);

        if (isAlreadyExist)
        {
            throw new DuplicateDeviceInfoException("DeviceInfoAlready exist for the customer");
        }

        var deviceEndpoint = await _pushNotificationEndpointService.CreateDeviceEndpoint(request.token);
        

        var deviceInfo = new DeviceInfo.Entities.DeviceInfo()
        {
            token = request.token,
            customerDeviceEndpoint = deviceEndpoint,
            customerId = request.customerId
        };

        var savedDeviceInfo = await _deviceInfoService.SaveCustomerDeviceInfo(deviceInfo);
        return new JsonResult(savedDeviceInfo)
        {
            StatusCode = (int)HttpStatusCode.Created
        };
    }

    [HttpPost("/publish-message")]
    public async Task<JsonResult> PulishMessage([FromBody] PushNotificationRequestDto request)
    {
        await _pushNotificationEndpointService.SendPushNotificationToDevice(request.message, request.customerId, request.title);
        return new JsonResult(new object())
        {
            StatusCode = (int)HttpStatusCode.OK
        };
    }

}