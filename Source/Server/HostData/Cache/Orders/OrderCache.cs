using Shared.Data;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory;
using System.Collections.Concurrent;

namespace HostData.Cache.Orders;

internal class OrderCache : IOrderCache
{
    private readonly ConcurrentDictionary<Guid, IOrder> _ordersCache = new();

    public IReadOnlyCollection<IOrder> Orders => _ordersCache.Values.ToList();

    public IOrder GetOrderById(Guid orderId)
    {
        if (_ordersCache.TryGetValue(orderId, out var orderOnCache) is false)
            throw new EntityNotFoundException(orderId, nameof(IOrder));
        return orderOnCache;
    }

    public void AddOrUpdate(IOrder order, int version = 0)
    {
        if (_ordersCache.TryGetValue(order.Id, out var orderOnCache) is false)
            _ordersCache.TryAdd(order.Id, order);
        else
        {
            if (orderOnCache.Version + version != order.Version)
                throw new ArgumentException($"Order version {order.Version}. Order on cache version: {orderOnCache.Version + 1}");

            if (orderOnCache.Status.HasFlag(OrderStatus.Open) is false)
                throw new CantChangeAndRemoveOrderException(orderOnCache);

            _ordersCache.TryUpdate(order.Id, order, orderOnCache);
        }
    }

    public IOrder RemoveOrder(Guid orderId)
    {
        var order = GetOrderById(orderId);
        if (order.Status.HasFlag(OrderStatus.Open) is false)
            throw new CantChangeAndRemoveOrderException(order);

        if (order.IsDeleted is true)
            throw new CantRemoveDeletedItemException(order.Id);

        var orderDto = OrderFactory.CreateDto(order);
        orderDto = orderDto with { IsDeleted = true, Status = OrderStatus.Deleted };

        AddOrUpdate(OrderFactory.Create(orderDto));
        return GetOrderById(orderId);
    }
}
