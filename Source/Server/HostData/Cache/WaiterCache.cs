using Shared.Data;
using Shared.Exceptions;
using System.Collections.Concurrent;

namespace HostData.Cache;

internal class WaiterCache : IBaseCache<IWaiter>
{
    private readonly ConcurrentDictionary<Guid, IWaiter> _waitersCache = new();

    public IReadOnlyCollection<IWaiter> Values => _waitersCache.Values.ToList();

    public void AddOrUpdate(IWaiter waiter)
    {
        if (_waitersCache.TryGetValue(waiter.Id, out var waiterOnCache) is false)
            _waitersCache.TryAdd(waiter.Id, waiter);
        else
            _waitersCache.TryUpdate(waiter.Id, waiter, waiterOnCache);
    }

    public IWaiter GetById(Guid waiterId)
    {
        if (_waitersCache.TryGetValue(waiterId, out var returnWaiter) is false)
            throw new EntityNotFoundException(waiterId, nameof(IWaiter));
        return returnWaiter;
    }

    public IWaiter RemoveById(Guid waiterId)
    {
        if (_waitersCache.TryRemove(waiterId, out var returnWaiter) is false)
            throw new EntityNotFoundException(waiterId, nameof(IWaiter));

        return returnWaiter;
    }
}