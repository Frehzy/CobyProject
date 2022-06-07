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

public class OrderModule : BaseModule
{
    private readonly IConfigCache _configCache;
    private readonly OrderController _orderController;

    public OrderModule(IOrderCache orderCache, IConfigCache configCache) : base()
    {
        _configCache = configCache;
        _orderController = new(orderCache);

        Get("/orders/{orderId}", parameters =>
        {
            try
            {
                Log.Information(CreateLogByContext(Context));
                var order = _orderController.GetOrderById(parameters.orderId);

                var returnJson = JsonSerializer.Serialize<OrderDto>(order);
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

        Get("/orders", parameters =>
        {
            Log.Information(CreateLogByContext(Context));
            var orders = _orderController.GetOrders();

            var returnJson = JsonSerializer.Serialize(orders);
            Log.Information(CreateReturnLog(returnJson));
            return returnJson;
        });

        Get("/order/create/{waiterId}/{tableId}", parameters =>
        {
            try
            {
                Log.Information(CreateLogByContext(Context));
                var waiterId = parameters.waiterId;
                var tableId = parameters.tableId;
                var order = _orderController.CreateOrder(waiterId, tableId);

                var returnJson = JsonSerializer.Serialize<OrderDto>(order);
                Log.Information(CreateReturnLog(returnJson));
                return returnJson;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        });

        Post("/order/remove", parameters =>
        {
            try
            {
                Log.Information(CreateLogByContext(Context));
                var json = Request.Body.AsString();
                var order = JsonSerializer.Deserialize<OrderDto>(json);
                _orderController.RemoveOrderById(order.Id);

                var returnJson = JsonSerializer.Serialize(order);
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

        Post("/order/submitChanges", parameters =>
        {
            try
            {
                Log.Information(CreateLogByContext(Context));
                var json = Request.Body.AsString();
                var obj = JsonSerializer.Deserialize<SessionDto>(json);
                var order = _orderController.SubmitChanges(obj);

                var returnJson = JsonSerializer.Serialize(order);
                Log.Information(CreateReturnLog(returnJson));
                return returnJson;
            }
            catch (EntityNotFoundException ex)
            {
                var json = JsonSerializer.Serialize(ex.CreateDictionary(), CreateSerializerOptions());
                Log.Error(ex, json);
                return CreateExceptionResponse(json, nameof(EntityNotFoundException));
            }
            catch (InvalidSessionException ex)
            {
                var json = JsonSerializer.Serialize(ex.CreateDictionary(), CreateSerializerOptions());
                Log.Error(ex, json);
                return CreateExceptionResponse(json, nameof(InvalidSessionException));
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        });
    }
}
