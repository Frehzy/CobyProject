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

public class OrderModule : BaseModule
{
    private readonly ILogger _logger = Log.CreateLogger<OrderModule>();
    private readonly IConfigCache _configCache;
    private readonly OrderController _orderController;

    public OrderModule(IOrderCache orderCache, IConfigCache configCache) : base()
    {
        _configCache = configCache;
        _orderController = new(_logger, orderCache);

        Get("/orders/{orderId}", parameters =>
        {
            try
            {
                _logger.LogInformation(Log.CreateLog(Context));
                var order = _orderController.GetOrderById(parameters.orderId);
                return JsonSerializer.Serialize<OrderDto>(order);
            }
            catch (EntityNotFoundException ex)
            {
                var json = JsonSerializer.Serialize(ex.CreateDictionary());
                _logger.LogWarning(json);
                return CreateExceptionResponse(json, nameof(EntityNotFoundException));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        });

        Get("/orders", parameters =>
        {
            _logger.LogInformation(Log.CreateLog(Context));
            var orders = _orderController.GetOrders();
            return JsonSerializer.Serialize(orders);
        });

        Get("/order/create/{waiterId}/{tableId}", parameters =>
        {
            try
            {
                _logger.LogInformation(Log.CreateLog(Context));
                var waiterId = parameters.waiterId;
                var tableId = parameters.tableId;
                var order = _orderController.CreateOrder(waiterId, tableId);
                return JsonSerializer.Serialize<OrderDto>(order);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
        });

        Post("/order/remove", parameters =>
        {
            try
            {
                _logger.LogInformation(Log.CreateLog(Context));
                var json = Request.Body.AsString();
                var order = JsonSerializer.Deserialize<OrderDto>(json);
                _orderController.RemoveOrderById(order.Id);
                return JsonSerializer.Serialize(order);
            }
            catch (EntityNotFoundException ex)
            {
                var json = JsonSerializer.Serialize(ex.CreateDictionary());
                _logger.LogWarning(json);
                return CreateExceptionResponse(json, nameof(EntityNotFoundException));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
        });

        Post("/order/submitChanges", parameters =>
        {
            try
            {
                _logger.LogInformation(Log.CreateLog(Context));
                var json = Request.Body.AsString();
                var obj = JsonSerializer.Deserialize<SessionDto>(json);
                var order = _orderController.SubmitChanges(obj);
                return JsonSerializer.Serialize(order);
            }
            catch (EntityNotFoundException ex)
            {
                var json = JsonSerializer.Serialize(ex.CreateDictionary());
                _logger.LogWarning(json);
                return CreateExceptionResponse(json, nameof(EntityNotFoundException));
            }
            catch (InvalidSessionException ex)
            {
                var json = JsonSerializer.Serialize(ex.CreateDictionary());
                _logger.LogWarning(json);
                return CreateExceptionResponse(json, nameof(InvalidSessionException));
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
        });
    }
}
