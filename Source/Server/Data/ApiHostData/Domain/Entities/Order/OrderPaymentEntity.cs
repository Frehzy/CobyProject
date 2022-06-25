using Shared.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiHostData.Domain.Entities.Order;

public class OrderPaymentEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OrderEntityId { get; set; } = Guid.NewGuid();

    [JsonIgnore]
    [ForeignKey(nameof(OrderEntityId))]
    public virtual OrderEntity OrderEntity { get; set; }

    public Guid PaymentTypeEntityId { get; set; }

    public decimal Sum { get; set; }

    public PaymentStatus Status { get; set; }

    public OrderPaymentEntity() : base() { }
}