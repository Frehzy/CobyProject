using Shared.Data;
using Shared.Data.Enum;

namespace ApiModule.Operations.Contracts;

public interface IPaymentTypeOperation
{
    public IReadOnlyList<IPaymentType> GetPaymentTypes();

    public IPaymentType GetPaymentById(Guid paymentTypeId);

    public IPaymentType CreatePaymentType(ICredentials credentials, string name, PaymentTypeKind kind, bool needOpenCashRegister);

    public bool RemovePaymentType(ICredentials credentials, IPaymentType paymentType);
}