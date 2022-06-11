using Shared.Data;
using Shared.Exceptions;
using System.Collections.Concurrent;

namespace HostData.Cache.Discounts;

internal class DiscountCache : IDiscountCache
{
    private readonly ConcurrentDictionary<Guid, IDiscount> _discountsCache = new();

    public IReadOnlyCollection<IDiscount> Discounts => _discountsCache.Values.ToList();

    public void AddOrUpdate(IDiscount discount)
    {
        if (_discountsCache.TryGetValue(discount.Id, out var discountOnCache) is false)
            _discountsCache.TryAdd(discount.Id, discount);
        else
            _discountsCache.TryUpdate(discount.Id, discount, discountOnCache);
    }

    public IDiscount GetTableById(Guid discountId)
    {
        var result = _discountsCache.TryGetValue(discountId, out var discountOnCache);
        return result is true
            ? discountOnCache
            : throw new EntityNotFoundException(discountId, nameof(IDiscount));
    }

    public IDiscount RemoveDiscount(Guid discountId)
    {
        if (_discountsCache.TryRemove(discountId, out var returnDiscount) is false)
            throw new EntityNotFoundException(discountId, nameof(ITable));

        return returnDiscount;
    }
}