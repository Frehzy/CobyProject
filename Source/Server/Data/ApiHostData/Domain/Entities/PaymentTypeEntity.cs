using Shared.Data.Enum;
using SharedData.Entities.Implementation;

namespace ApiHostData.Domain.Entities;

public class PaymentTypeEntity : BaseEntity
{
    public string Name { get; set; }

    public PaymentTypeKind Kind { get; set; }

    public bool NeedOpenCashBox { get; set; }

    public PaymentTypeEntity() { }
}