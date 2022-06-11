using Shared.Data;

namespace HostData.Cache.Discounts;

public interface IDiscountCache
{
    IReadOnlyCollection<IDiscount> Discounts { get; }

    IDiscount GetDiscountById(Guid discountId);

    void AddOrUpdate(IDiscount discount);

    IDiscount RemoveDiscount(Guid discountId);
}