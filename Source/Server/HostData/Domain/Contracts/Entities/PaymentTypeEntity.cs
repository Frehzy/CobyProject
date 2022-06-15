using Shared.Data.Enum;

namespace HostData.Domain.Contracts.Entities;

public class PaymentTypeEntity : BaseEntity
{
    public string Name { get; set; }

    public PaymentTypeKind Kind { get; set; }

    public bool NeedOpenCashBox { get; set; }

    public PaymentTypeEntity() { }
}