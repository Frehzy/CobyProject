using Shared.Data;
using System.Collections.Concurrent;

namespace HostData.Cache.Orders;

internal class OrderCache : IOrderCache
{
    private readonly ConcurrentDictionary<Guid, IOrder> _ordersCache = new();

    public IReadOnlyCollection<IOrder> Orders => _ordersCache.Values.ToList();

    public IOrder GetOrderById(Guid orderId)
    {
        var result = _ordersCache.TryGetValue(orderId, out var orderOnCache);
        return result is true
            ? orderOnCache
            : throw new InvalidOperationException();
    }

    public void AddOrUpdate(IOrder order, int version = 0)
    {
        if (_ordersCache.TryGetValue(order.Id, out var orderOnCache) is false)
            _ordersCache.TryAdd(order.Id, order);
        else
        {
            if (orderOnCache.Version + version != order.Version)
                throw new ArgumentException($"Order version {order.Version}. Order on cache version: {orderOnCache.Version + 1}");

            _ordersCache.TryUpdate(order.Id, order, orderOnCache);
        }
    }

    public bool RemoveOrder(Guid orderId)
    {
        if (_ordersCache.TryRemove(orderId, out _) is false)
            throw new InvalidOperationException();

        return true;
    }
}
