using HostData.Cache.Config;
using HostData.Cache.Orders;
using HostData.Controllers;
using HostData.Controllers.LogFactory;
using HostData.Model;
using HostData.Serialization;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Extensions;

namespace HostData.Modules;

public class GuestModule : NancyModule
{
    private readonly ILogger _logger = Log.CreateLogger<GuestModule>();
    private readonly IConfigCache _configCache;
    private readonly GuestController _guestController;

    public GuestModule(IOrderCache orderCache, IConfigCache configCache) : base("/")
    {
        _configCache = configCache;
        _guestController = new(_logger, orderCache);

        Post("/{orderId}/guest/add", parameters =>
        {
            _logger.LogInformation(Log.CreateLog(Context));
            var orderId = parameters.orderId;
            var json = this.Request.Body.AsString();
            var obj = Json.Deserialize<Session>(json, _configCache.OrganizationId.ToString());
            var session = _guestController.AddGuest(orderId, obj);
            return Json.Serialization<Session>(session, _configCache.OrganizationId.ToString());
        });
    }
}