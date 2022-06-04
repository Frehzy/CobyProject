using Api.Data.Order;
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

        return _orderCache.GetOrderById(orderId);
    }

    public List<Order> GetOrders() => _orderCache.Orders.ToList();

    public Order CreateOrder(dynamic waiterId, dynamic tableId)
    {
        if (Guid.TryParse(waiterId.ToString(), out Guid wId) is false)
            throw new ArgumentException(nameof(waiterId));

        if (Guid.TryParse(tableId.ToString(), out Guid tId) is false)
            throw new ArgumentException(nameof(tableId));

        var order = new Order(Guid.NewGuid(), tId, wId, DateTime.Now, null, OrderStatus.Open, 1);
        _orderCache.AddOrUpdate(order);
        return order;
    }

    public Order SubmitChanges(Session session)
    {
        if (session.Orders.Count <= 0)
            throw new InvalidOperationException(nameof(session.Orders));

        var lastOrder = session.Orders.OrderByDescending(x => x.Version).First();
        _orderCache.AddOrUpdate(lastOrder, session.Orders.Count);
        return _orderCache.GetOrderById(lastOrder.Id);
    }

    public bool RemoveOrderById(dynamic id)
    {
        if (Guid.TryParse(id.ToString(), out Guid orderId) is false)
            throw new ArgumentException(nameof(id));

        return _orderCache.RemoveOrder(orderId);
    }
}