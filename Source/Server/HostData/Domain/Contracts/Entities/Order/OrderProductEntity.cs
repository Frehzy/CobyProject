using Shared.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HostData.Domain.Contracts.Entities.Order;

public class OrderProductEntity : BaseEntity
{
    [JsonIgnore]
    [ForeignKey(nameof(Id))]
    public virtual ProductItemEntity ProductItem { get; set; }

    public virtual OrderEntity Order { get; set; }

    public Guid GuestId { get; set; }

    public Guid WaiterId { get; set; }

    public DateTime? PrintTime { get; set; }

    public string? Comment { get; set; }

    public ProductStatus Status { get; set; }

    public OrderProductEntity() : base() { }
}