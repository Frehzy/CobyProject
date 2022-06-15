using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HostData.Domain.Contracts.Entities.Order;

public class OrderProductEntity : BaseEntity
{
    [JsonIgnore]
    [ForeignKey(nameof(Id))]
    public virtual ProductEntity ProductEntity { get; set; }

    public virtual OrderEntity Order { get; set; }

    public OrderProductEntity() : base() { }
}