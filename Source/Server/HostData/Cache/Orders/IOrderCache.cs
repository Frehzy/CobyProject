using Shared.Data;

namespace HostData.Cache.Orders;

public interface IOrderCache
{
    IReadOnlyCollection<IOrder> Orders { get; }

    IOrder GetOrderById(Guid orderId);

    void AddOrUpdate(IOrder order, int version = 0);

    IOrder RemoveOrder(Guid orderId);
}