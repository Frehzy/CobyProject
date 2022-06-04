using HostData.Model;

namespace HostData.Cache.Orders;

public interface IOrderCache
{
    IReadOnlyCollection<Order> Orders { get; }

    Order GetOrderById(Guid orderId);

    void AddOrUpdate(Order order, int version = 0);

    bool RemoveOrder(Guid orderId);
}