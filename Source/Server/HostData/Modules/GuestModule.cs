using HostData.Cache.Config;
using HostData.Cache.Orders;
using HostData.Controllers;
using HostData.Controllers.LogFactory;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Extensions;
using Shared.Exceptions;
using Shared.Factory.Dto;
using System.Text.Json;

namespace HostData.Modules;

public class GuestModule : BaseModule
{
    private readonly ILogger _logger = Log.CreateLogger<GuestModule>();
    private readonly IConfigCache _configCache;
    private readonly GuestController _guestController;

    public GuestModule(IOrderCache orderCache, IConfigCache configCache) : base()
    {
        _configCache = configCache;
        _guestController = new(_logger, orderCache);

        Post("/{orderId}/guest/add", parameters =>
        {
            try
            {
                _logger.LogInformation(Log.CreateLog(Context));
                var orderId = parameters.orderId;
                var json = Request.Body.AsString();
                var obj = JsonSerializer.Deserialize<SessionDto>(json);
                var session = _guestController.AddGuest(orderId, obj);
                return JsonSerializer.Serialize<SessionDto>(session);
            }
            catch (EntityNotFoundException ex)
            {
                var json = JsonSerializer.Serialize(ex.CreateDictionary());
                return CreateExceptionResponse(json, nameof(EntityNotFoundException));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        });
    }
}