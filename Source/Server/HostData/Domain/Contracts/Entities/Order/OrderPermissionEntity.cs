using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HostData.Domain.Contracts.Entities.Order;

public class OrderPermissionEntity : BaseEntity
{
    [JsonIgnore]
    [ForeignKey(nameof(Id))]
    public PermissionEntity PermissionEntity { get; set; }

    public virtual OrderWaiterEntity Waiter { get; set; }

    public OrderPermissionEntity() : base() { }
}