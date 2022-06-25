using Shared.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiHostData.Domain.Entities.Order;

public class OrderProductEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OrderEntityId { get; set; } = Guid.NewGuid();

    [JsonIgnore]
    [ForeignKey(nameof(OrderEntityId))]
    public virtual OrderEntity OrderEntity { get; set; }

    public Guid ProductItemEntityId { get; set; }

    public Guid OrderGuestEntityId { get; set; }

    public Guid OrderWaiterEntityId { get; set; }

    public DateTime? PrintTime { get; set; }

    public string? Comment { get; set; }

    public ProductStatus Status { get; set; }

    public OrderProductEntity() : base() { }
}