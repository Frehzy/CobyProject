using Shared.Data;
using Shared.Data.Enum;

namespace Shared.Factory.InternalModel;

internal class Payment : IPayment
{
    public Guid Id { get; set; }

    public decimal Sum { get; set; }

    public IPaymentType Type { get; set; }

    public PaymentStatus Status { get; set; }

    public Payment() { }

    public Payment(Guid id, decimal sum, PaymentType type, PaymentStatus status)
    {
        Id = id;
        Sum = sum;
        Type = type;
        Status = status;
    }
}
