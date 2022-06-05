using HostData.Cache.Config;
using HostData.Cache.Orders;
using HostData.Controllers;
using HostData.Controllers.LogFactory;
using HostData.Serialization;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Extensions;
using Shared.Factory.Dto;

namespace HostData.Modules;

public class OrderModule : NancyModule
{
    private readonly ILogger _logger = Log.CreateLogger<OrderModule>();
    private readonly IConfigCache _configCache;
    private readonly OrderController _orderController;

    public OrderModule(IOrderCache orderCache, IConfigCache configCache) : base("/")
    {
        _configCache = configCache;
        _orderController = new(_logger, orderCache);

        Get("/orders/{orderId}", parameters =>
        {
            _logger.LogInformation(Log.CreateLog(Context));
            var order = _orderController.GetOrderById(parameters.orderId);
            return Json.Serialization<OrderDto>(order, _configCache.OrganizationId.ToString());
        });

        Get("/orders", parameters =>
        {
            _logger.LogInformation(Log.CreateLog(Context));
            var orders = _orderController.GetOrders();
            return Json.Serialization<OrderDto>(orders, _configCache.OrganizationId.ToString());
        });

        Get("/order/create/{waiterId}/{tableId}", parameters =>
        {
            var waiterId = parameters.waiterId;
            var tableId = parameters.tableId;
            _logger.LogInformation(Log.CreateLog(Context));
            var order = _orderController.CreateOrder(waiterId, tableId);
            return Json.Serialization<OrderDto>(order, _configCache.OrganizationId.ToString());
        });

        Post("/order/remove", parameters =>
        {
            _logger.LogInformation(Log.CreateLog(Context));
            var json = Request.Body.AsString();
            var order = Json.Deserialize<OrderDto>(json, _configCache.OrganizationId.ToString());
            _orderController.RemoveOrderById(order.Id);
            return Json.Serialization(order, _configCache.OrganizationId.ToString());
        });

        Post("/order/submitChanges", parameters =>
        {
            _logger.LogInformation(Log.CreateLog(Context));
            var json = Request.Body.AsString();
            var obj = Json.Deserialize<SessionDto>(json, _configCache.OrganizationId.ToString());
            var order = _orderController.SubmitChanges(obj);
            return Json.Serialization(order, _configCache.OrganizationId.ToString());
        });
    }
}
