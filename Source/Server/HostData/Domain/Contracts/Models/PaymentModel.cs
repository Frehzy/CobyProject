using Shared.Data.Enum;

namespace HostData.Domain.Contracts.Models;

public class PaymentModel : BaseModel
{
    public decimal Sum { get; set; }

    public PaymentTypeModel Type { get; set; }

    public PaymentStatus Status { get; set; }

    public PaymentModel() : base() { }
}