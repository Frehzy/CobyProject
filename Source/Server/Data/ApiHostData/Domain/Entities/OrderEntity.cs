using ApiHostData.Domain.Entities.Order;
using Shared.Data.Enum;
using SharedData.Entities.Implementation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiHostData.Domain.Entities;

public class OrderEntity : BaseEntity
{
    public int Number { get; set; }

    [Required]
    [JsonIgnore]
    [InverseProperty(nameof(OrderTableEntity.OrderEntity))]
    public virtual OrderWaiterEntity OrderWaiterEntity { get; set; }

    [Required]
    [JsonIgnore]
    [InverseProperty(nameof(OrderTableEntity.OrderEntity))]
    public virtual ICollection<OrderTableEntity> OrderTableEntities { get; set; }

    [Required]
    [JsonIgnore]
    [InverseProperty(nameof(OrderTableEntity.OrderEntity))]
    public virtual ICollection<OrderGuestEntity>? OrderGuestEntities { get; set; }

    [Required]
    [JsonIgnore]
    [InverseProperty(nameof(OrderTableEntity.OrderEntity))]
    public virtual ICollection<OrderProductEntity>? OrderProductEntities { get; set; }

    [Required]
    [JsonIgnore]
    [InverseProperty(nameof(OrderTableEntity.OrderEntity))]
    public virtual ICollection<OrderDiscountEntity>? OrderDiscountEntities { get; set; }

    [Required]
    [JsonIgnore]
    [InverseProperty(nameof(OrderTableEntity.OrderEntity))]
    public virtual ICollection<OrderPaymentEntity>? OrderPaymentEntities { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? CloseTime { get; set; }

    public OrderEntity() : base()
    {
        OrderTableEntities = new List<OrderTableEntity>();
        OrderGuestEntities = new List<OrderGuestEntity>();
        OrderProductEntities = new List<OrderProductEntity>();
        OrderDiscountEntities = new List<OrderDiscountEntity>();
        OrderPaymentEntities = new List<OrderPaymentEntity>();
    }
}