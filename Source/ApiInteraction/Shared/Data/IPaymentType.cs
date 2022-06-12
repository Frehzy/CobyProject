using Shared.Data.Enum;

namespace Shared.Data;

public interface IPaymentType
{
    public Guid Id { get; }

    public string Name { get; }

    public PaymentTypeKind Kind { get; }

    public bool NeedOpenCashBox { get; } //нужно ли открывать кассовый ящик

    public bool IsDeleted { get; }
}