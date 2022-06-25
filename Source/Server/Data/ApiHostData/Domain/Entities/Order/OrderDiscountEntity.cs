using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiHostData.Domain.Entities.Order;

public class OrderDiscountEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OrderEntityId { get; set; } = Guid.NewGuid();

    [JsonIgnore]
    [ForeignKey(nameof(OrderEntityId))]
    public virtual OrderEntity OrderEntity { get; set; }

    public Guid DiscountTypeEntityId { get; set; }

    public decimal DiscountSum { get; set; }

    public bool IsActive { get; set; }

    public OrderDiscountEntity() : base() { }
}