using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiHostData.Domain.Entities.Order;

public class OrderTableEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OrderEntityId { get; set; } = Guid.NewGuid();

    [JsonIgnore]
    [ForeignKey(nameof(OrderEntityId))]
    public virtual OrderEntity OrderEntity { get; set; }

    public Guid TableEntityId { get; set; }

    public OrderTableEntity() : base() { }
}