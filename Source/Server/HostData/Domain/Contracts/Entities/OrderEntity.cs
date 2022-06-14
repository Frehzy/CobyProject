using Shared.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostData.Domain.Contracts.Entities;

public class OrderEntity : BaseEntity
{
    public int Number { get; set; }

    public Guid WaiterId { get; set; }

    [ForeignKey(nameof(WaiterId))]
    public virtual WaiterEntity Waiter { get; set; }

    public virtual ICollection<TableEntity> Tables { get; set; }

    public virtual ICollection<GuestEntity> Guests { get; set; }

    public virtual ICollection<ProductEntity> Products { get; set; }

    public virtual ICollection<DiscountEntity> Discounts { get; set; }

    public virtual ICollection<PaymentEntity> Payments { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? CloseTime { get; set; }
}