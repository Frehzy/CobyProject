using Shared.Data;

namespace Api.Operations.Contracts;

public interface IDiscountTypeOperation
{
    public IReadOnlyList<IDiscountType> GetDiscountTypes();

    public IDiscountType GetDiscountTypeById(Guid discountTypeId);

    public IDiscountType CreateDiscountType(ICredentials credentials, string name);

    public bool RemoveDiscountType(ICredentials credentials, IDiscountType discountType);
}