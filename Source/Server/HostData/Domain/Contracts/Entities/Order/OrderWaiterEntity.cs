using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HostData.Domain.Contracts.Entities.Order;

public class OrderWaiterEntity : BaseEntity
{
    [JsonIgnore]
    [ForeignKey(nameof(Id))]
    public virtual WaiterEntity WaiterEntity { get; set; }

    public virtual OrderEntity Order { get; set; }

    public OrderWaiterEntity() : base() { }
}