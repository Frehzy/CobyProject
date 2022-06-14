using Shared.Data.Enum;

namespace HostData.Domain.Contracts.Models;

public class PaymentModel
{
    public Guid Id { get; set; }

    public decimal Sum { get; set; }

    public PaymentTypeModel Type { get; set; }

    public PaymentStatus Status { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.Now;
}