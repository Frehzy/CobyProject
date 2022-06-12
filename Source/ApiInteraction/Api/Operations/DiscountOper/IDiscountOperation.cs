using Shared.Data;

namespace Api.Operations.DiscountOper;

public interface IDiscountOperation
{
    public IReadOnlyList<IDiscount> AddDiscount(ICredentials credentials, IDiscount discount, ref ISession session);

    public IReadOnlyList<IDiscount> RemoveDiscount(ICredentials credentials, IDiscount discount, ref ISession session);

    public IReadOnlyList<IDiscount> GetDiscount();
}