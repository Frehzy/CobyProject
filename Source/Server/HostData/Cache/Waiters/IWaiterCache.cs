using Shared.Data;

namespace HostData.Cache.Waiters;

public interface IWaiterCache
{
    IReadOnlyCollection<IWaiter> Waiters { get; }

    IWaiter GetWaiterById(Guid waiterId);

    void AddOrUpdate(IWaiter waiter);

    IWaiter RemoveWaiter(Guid waiterId);
}