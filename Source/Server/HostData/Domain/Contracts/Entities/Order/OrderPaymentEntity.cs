using Shared.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HostData.Domain.Contracts.Entities.Order;

public class OrderPaymentEntity : BaseEntity
{
    public decimal Sum { get; set; }

    public PaymentStatus Status { get; set; }

    public Guid PaymentTypeId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(PaymentTypeId))]
    public virtual OrderPaymentTypeEntity Type { get; set; }

    public virtual OrderEntity Order { get; set; }

    public OrderPaymentEntity() : base() { }
}