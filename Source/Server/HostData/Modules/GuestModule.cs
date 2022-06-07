using HostData.Cache.Config;
using HostData.Cache.Orders;
using HostData.Controllers;
using Nancy;
using Nancy.Extensions;
using Serilog;
using Shared.Exceptions;
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
            try
            {
                Log.Information(CreateLogByContext(Context));
                var orderId = parameters.orderId;
                var json = Request.Body.AsString();
                var obj = JsonSerializer.Deserialize<SessionDto>(json);
                var session = _guestController.AddGuest(orderId, obj);

                var returnJson = JsonSerializer.Serialize<SessionDto>(session);
                Log.Information(CreateReturnLog(returnJson));
                return returnJson;
            }
            catch (EntityNotFoundException ex)
            {
                var json = JsonSerializer.Serialize(ex.CreateDictionary(), CreateSerializerOptions());
                Log.Error(ex, json);
                return CreateExceptionResponse(json, nameof(EntityNotFoundException));
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        });
    }
}