using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HostData.Domain.Contracts.Entities.Order;

public class OrderGuestEntity : BaseEntity
{
    [JsonIgnore]
    [ForeignKey(nameof(Id))]
    public virtual GuestEntity Guest { get; set; }

    public virtual OrderEntity Order { get; set; }

    public OrderGuestEntity() : base() { }
}