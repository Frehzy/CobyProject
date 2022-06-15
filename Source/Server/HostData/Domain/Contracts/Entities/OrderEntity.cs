using HostData.Domain.Contracts.Entities.Order;
using Shared.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HostData.Domain.Contracts.Entities;

public class OrderEntity : BaseEntity
{
    public int Number { get; set; }

    public Guid WaiterId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(WaiterId))]
    public virtual OrderWaiterEntity Waiter { get; set; }

    public virtual List<Guid> TablesId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(TablesId))]
    public virtual ICollection<OrderTableEntity> Tables { get; set; }

    public virtual List<Guid> GuestsId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(GuestsId))]
    public virtual ICollection<OrderGuestEntity> Guests { get; set; }

    public virtual List<Guid> ProductsId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(ProductsId))]
    public virtual ICollection<OrderProductEntity> Products { get; set; }

    public virtual List<Guid> DiscountsId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(DiscountsId))]
    public virtual ICollection<OrderDiscountEntity> Discounts { get; set; }

    public virtual List<Guid> PaymentsId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(PaymentsId))]
    public virtual ICollection<OrderPaymentEntity> Payments { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? CloseTime { get; set; }

    public OrderEntity() : base() { }
}