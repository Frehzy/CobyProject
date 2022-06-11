using HostData.Cache.Config;
using HostData.Cache.Orders;
using HostData.Cache.Waiters;
using HostData.Controllers;
using Nancy;
using Nancy.Extensions;
using Shared.Factory.Dto;
using System.Text.Json;

namespace HostData.Modules;

public class GuestModule : BaseModule
{
    private readonly IConfigCache _configCache;
    private readonly GuestController _guestController;

    public GuestModule(IOrderCache orderCache, IWaiterCache waiterCache, IConfigCache configCache) : base()
    {
        _configCache = configCache;
        _guestController = new(orderCache, waiterCache);

        Post("/{orderId}/guest/add/{credentialsId}", parameters =>
        {
            var orderId = parameters.orderId;
            var credentialsId = parameters.credentialsId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _guestController.AddGuest(orderId, credentialsId, obj));
        });

        Post("/{orderId}/guest/remove/{credentialsId}/{guestId}", parameters =>
        {
            var orderId = parameters.orderId;
            var credentialsId = parameters.credentialsId;
            var guestId = parameters.guestId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _guestController.RemoveGuest(orderId, credentialsId, guestId, obj));
        });
    }
}