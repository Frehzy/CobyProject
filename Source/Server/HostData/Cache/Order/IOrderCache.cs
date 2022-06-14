using Shared.Data;

namespace HostData.Cache.Order;

public interface IOrderCache
{
    IReadOnlyCollection<IOrder> Values { get; }

    IOrder GetById(Guid id);

    void AddOrUpdate(IOrder instance, int version = 0);

    IOrder RemoveById(Guid id);
}