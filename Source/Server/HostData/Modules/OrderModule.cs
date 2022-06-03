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

public class OrderModule : NancyModule
{
    private readonly ILogger _logger = Log.CreateLogger<OrderModule>();
    private readonly IConfigCache _configCache;
    private readonly OrderController _orderController;

    public OrderModule(IOrderCache orderCache, IConfigCache configCache) : base("/")
    {
        _configCache = configCache;
        _orderController = new OrderController(_logger, orderCache);

        Get("/orders/{orderId}", parameters =>
        {
            _logger.LogInformation(Log.CreateLog(Context));
            var order = _orderController.GetOrderById(parameters.orderId);
            return Json.Serialization<Order>(order, _configCache.OrganizationId.ToString());
        });

        Get("/orders", parameters =>
        {
            _logger.LogInformation(Log.CreateLog(Context));
            var orders = _orderController.GetOrders();
            return Json.Serialization<Order>(orders, _configCache.OrganizationId.ToString());
        });

        Post("add/order", parameters =>
        {
            _logger.LogInformation(Log.CreateLog(Context));
            var json = this.Request.Body.AsString();
            var obj = Json.Deserialize<Order>(json, _configCache.OrganizationId.ToString());
            return _orderController.AddOrUpdate(obj).ToString();
        });
    }
}
