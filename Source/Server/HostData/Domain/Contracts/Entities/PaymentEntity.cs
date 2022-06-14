using Shared.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostData.Domain.Contracts.Entities;

public class PaymentEntity : BaseEntity
{
    public decimal Sum { get; set; }

    public Guid PaymentTypeId { get; set; }

    [ForeignKey(nameof(PaymentTypeId))]
    public virtual PaymentTypeEntity Type { get; set; }

    public PaymentStatus Status { get; set; }

    public virtual OrderEntity Order { get; set; }
}