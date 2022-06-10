using HostData.Cache.Config;
using HostData.Cache.Orders;
using HostData.Controllers;
using Nancy;
using Nancy.Extensions;
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
            return Execute<OrderDto>(Context, () => _orderController.GetOrderById(parameters.orderId));
        });

        Get("/orders", parameters =>
        {
            return Execute(Context, () => _orderController.GetOrders());
        });

        Get("/order/create/{waiterId}/{tableId}", parameters =>
        {
            var waiterId = parameters.waiterId;
            var tableId = parameters.tableId;
            return Execute<OrderDto>(Context, () => _orderController.CreateOrder(waiterId, tableId));
        });

        Post("/order/remove", parameters =>
        {
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<OrderDto>(json);
            return Execute(Context, () => _orderController.RemoveOrderById(obj.Id));
        });

        Post("/order/submitChanges", parameters =>
        {
            var json = Request.Body.AsString();
            var obj = JsonSerializer.Deserialize<SessionDto>(json);
            return Execute(Context, () => _orderController.SubmitChanges(obj));
        });
    }
}
