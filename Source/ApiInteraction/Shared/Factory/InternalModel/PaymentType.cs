using Shared.Data;
using Shared.Data.Enum;

namespace Shared.Factory.InternalModel;

internal class PaymentType : IPaymentType
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public PaymentTypeKind Kind { get; set; }

    public bool NeedOpenCashBox { get; set; }

    public bool IsDeleted { get; set; }

    public PaymentType() { }

    public PaymentType(Guid id, string name, PaymentTypeKind kind, bool needOpenCashBox, bool isDeleted)
    {
        Id = id;
        Name = name;
        Kind = kind;
        NeedOpenCashBox = needOpenCashBox;
        IsDeleted = isDeleted;
    }
}