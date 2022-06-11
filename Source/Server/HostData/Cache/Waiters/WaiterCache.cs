using Shared.Data;
using Shared.Exceptions;
using System.Collections.Concurrent;

namespace HostData.Cache.Waiters;

internal class WaiterCache : IWaiterCache
{
    private readonly ConcurrentDictionary<Guid, IWaiter> _waitersCache = new();

    public IReadOnlyCollection<IWaiter> Waiters => _waitersCache.Values.ToList();

    public void AddOrUpdate(IWaiter waiter)
    {
        if (_waitersCache.TryGetValue(waiter.Id, out var waiterOnCache) is false)
            _waitersCache.TryAdd(waiter.Id, waiter);
        else
            _waitersCache.TryUpdate(waiter.Id, waiter, waiterOnCache);
    }

    public IWaiter GetWaiterById(Guid waiterId)
    {
        var result = _waitersCache.TryGetValue(waiterId, out var waiterOnCache);
        return result is true
            ? waiterOnCache
            : throw new EntityNotFoundException(waiterId, nameof(IWaiter));
    }

    public IWaiter RemoveWaiter(Guid waiterId)
    {
        if (_waitersCache.TryRemove(waiterId, out var returnWaiter) is false)
            throw new EntityNotFoundException(waiterId, nameof(IWaiter));

        return returnWaiter;
    }
}