using Shared.Data.Enum;

namespace Shared.Data;

public interface IPayment
{
    public Guid Id { get; }

    public decimal Sum { get; }

    public IPaymentType Type { get; }

    public PaymentStatus Status { get; }

    public bool IsDeleted { get; }
}