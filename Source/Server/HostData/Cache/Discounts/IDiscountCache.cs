using Shared.Data;

namespace HostData.Cache.Discounts;

public interface IDiscountCache
{
    IReadOnlyCollection<IDiscount> Discounts { get; }

    IDiscount GetTableById(Guid discountId);

    void AddOrUpdate(IDiscount discount);

    IDiscount RemoveDiscount(Guid discountId);
}