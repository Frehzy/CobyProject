using Shared.Data;

namespace HostData.Cache.Payments;

public interface IPaymentTypeCache
{
    IReadOnlyCollection<IPaymentType> PaymentTypes { get; }

    IPaymentType GetPaymentTypeById(Guid paymentTypeId);

    void AddOrUpdate(IPaymentType paymentType);

    IPaymentType RemovePaymentType(Guid paymentTypeId);
}