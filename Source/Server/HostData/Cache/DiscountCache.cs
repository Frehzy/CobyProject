using Shared.Data;
using Shared.Exceptions;
using Shared.Factory;
using System.Collections.Concurrent;

namespace HostData.Cache;

internal class DiscountCache : IBaseCache<IDiscount>
{
    private readonly ConcurrentDictionary<Guid, IDiscount> _discountsCache = new();

    public IReadOnlyCollection<IDiscount> Values => _discountsCache.Values.ToList();

    public void AddOrUpdate(IDiscount discount)
    {
        if (_discountsCache.TryGetValue(discount.Id, out var discountOnCache) is false)
            _discountsCache.TryAdd(discount.Id, discount);
        else
            _discountsCache.TryUpdate(discount.Id, discount, discountOnCache);
    }

    public IDiscount GetById(Guid discountId)
    {
        if (_discountsCache.TryGetValue(discountId, out var returnDiscount) is false)
            throw new EntityNotFoundException(discountId, nameof(IDiscount));
        return returnDiscount;
    }

    public IDiscount RemoveById(Guid discountId)
    {
        var discount = GetById(discountId);

        if (discount.IsDeleted is true)
            throw new CantRemoveDeletedItemException(discount.Id);

        var discountDto = DiscountFactory.CreateDto(discount);
        discountDto = discountDto with { IsDeleted = true };

        AddOrUpdate(DiscountFactory.Create(discountDto));
        return GetById(discountId);
    }
}