using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HostData.Domain.Contracts.Entities.Order;

public class OrderTableEntity : BaseEntity
{
    [JsonIgnore]
    [ForeignKey(nameof(Id))]
    public virtual TableEntity TableEntity { get; set; }

    public virtual OrderEntity Order { get; set; }

    public OrderTableEntity() : base() { }
}