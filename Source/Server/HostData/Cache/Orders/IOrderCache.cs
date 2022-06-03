using Api.Data.Order;
using HostData.Model;

namespace HostData.Cache.Orders;

public interface IOrderCache
{
    IReadOnlyCollection<Order> Orders { get; }

    Order TryGetOrderById(Guid orderId);

    void AddOrUpdate(IOrder order);

    bool RemoveOrder(Guid orderId);
}