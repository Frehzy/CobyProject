using HostData.Cache.Config;
using HostData.Cache.Orders;
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

    public GuestModule(IOrderCache orderCache, IConfigCache configCache) : base()
    {
        _configCache = configCache;
        _guestController = new(orderCache);

        Post("/{orderId}/guest/add", parameters =>
        {
            var orderId = parameters.orderId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _guestController.AddGuest(orderId, obj));
        });

        Post("/{orderId}/guest/remove/{guestId}", parameters =>
        {
            var orderId = parameters.orderId;
            var guestId = parameters.guestId;
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute<SessionDto>(Context, () => _guestController.RemoveGuest(orderId, guestId, obj));
        });
    }
}