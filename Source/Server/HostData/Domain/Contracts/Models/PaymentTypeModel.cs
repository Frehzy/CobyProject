using Shared.Data.Enum;

namespace HostData.Domain.Contracts.Models;

public class PaymentTypeModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public PaymentTypeKind Kind { get; set; }

    public bool NeedOpenCashBox { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public bool IsDeleted { get; set; } = false;

    public PaymentTypeModel() { }
}