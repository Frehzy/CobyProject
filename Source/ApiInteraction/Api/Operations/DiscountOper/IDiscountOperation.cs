using Shared.Data;

namespace Api.Operations.DiscountOper;

public interface IDiscountOperation
{
    public IReadOnlyList<IDiscount> AddDiscount(IOrder order, ICredentials credentials, IDiscount discount, ref ISession session);

    public IReadOnlyList<IDiscount> RemoveDiscount(IOrder order, ICredentials credentials, IDiscount discount, ref ISession session);

    public IReadOnlyList<IDiscount> GetDiscount();
}