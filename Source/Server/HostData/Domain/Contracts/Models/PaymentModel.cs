using Shared.Data.Enum;

namespace HostData.Domain.Contracts.Models;

public class PaymentModel : BaseModel
{
    public decimal Sum { get; set; }

    public PaymentTypeModel Type { get; set; }

    public PaymentStatus Status { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public bool IsDeleted { get; set; } = false;

    public PaymentModel() { }
}