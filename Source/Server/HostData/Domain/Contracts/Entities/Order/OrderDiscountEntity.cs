using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HostData.Domain.Contracts.Entities.Order;

public class OrderDiscountEntity : BaseEntity
{
    [JsonIgnore]
    [ForeignKey(nameof(Id))]
    public virtual DiscountEntity Discount { get; set; }

    public virtual OrderEntity Order { get; set; }

    public OrderDiscountEntity() : base() { }
}