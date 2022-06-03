using HostData.Cache.Orders;
using HostData.Controllers.LogFactory;
using HostData.Model;
using Microsoft.Extensions.Logging;

namespace HostData.Controllers;

internal class OrderController : LoggerController
{
    private readonly IOrderCache _orderCache;

    public OrderController(ILogger logger, IOrderCache orderCache) : base(logger)
    {
        _orderCache = orderCache;
    }

    public Order GetOrderById(dynamic id)
    {
        if (Guid.TryParse(id.ToString(), out Guid orderId) is false)
            throw new ArgumentException(nameof(id));

        return _orderCache.TryGetOrderById(orderId);
    }

    public List<Order> GetOrders() => _orderCache.Orders.ToList();

    public int AddOrUpdate(Order order)
    {
        _orderCache.AddOrUpdate(order);
        return _orderCache.Orders.Count;
    }

    public bool RemoveOrderById(dynamic id)
    {
        if (Guid.TryParse(id.ToString(), out Guid orderId) is false)
            throw new ArgumentException(nameof(id));

        return _orderCache.RemoveOrder(orderId);
    }
}