using Shared.Data.Enum;

namespace HostData.Domain.Contracts.Models;

public class PaymentTypeModel : BaseModel
{
    public string Name { get; set; }

    public PaymentTypeKind Kind { get; set; }

    public bool NeedOpenCashBox { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public bool IsDeleted { get; set; } = false;

    public PaymentTypeModel() { }
}