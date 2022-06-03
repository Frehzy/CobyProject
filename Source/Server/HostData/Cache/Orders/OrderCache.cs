using Api.Data.Order;
using HostData.Factory;
using HostData.Model;
using System.Collections.Concurrent;

namespace HostData.Cache.Orders;

internal class OrderCache : IOrderCache
{
    private readonly ConcurrentDictionary<Guid, IOrder> _ordersCache = new();

    public IReadOnlyCollection<Order> Orders => _ordersCache.Values.Select(x => OrderFactory.CreateOrder(x)).ToList();

    public Order TryGetOrderById(Guid orderId)
    {
        var result = _ordersCache.TryGetValue(orderId, out var orderOnCache);
        return result is true
            ? OrderFactory.CreateOrder(orderOnCache)
            : throw new InvalidOperationException();
    }

    public void AddOrUpdate(IOrder order) =>
        _ordersCache.AddOrUpdate(order.OrderId, order, (oldKey, oldValue) => order);

    public bool RemoveOrder(Guid orderId)
    {
        if (_ordersCache.TryRemove(orderId, out _) is false)
            throw new InvalidOperationException();

        return true;
    }
}
