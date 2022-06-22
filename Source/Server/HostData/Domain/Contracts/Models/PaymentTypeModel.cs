using Shared.Data.Enum;

namespace HostData.Domain.Contracts.Models;

public class PaymentTypeModel : BaseModel
{
    public string Name { get; set; }

    public PaymentTypeKind Kind { get; set; }

    public bool NeedOpenCashBox { get; set; }

    public PaymentTypeModel() : base() { }
}