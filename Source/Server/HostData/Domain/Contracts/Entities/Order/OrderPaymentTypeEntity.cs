using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HostData.Domain.Contracts.Entities.Order;

public class OrderPaymentTypeEntity : BaseEntity
{
    [JsonIgnore]
    [ForeignKey(nameof(Id))]
    public virtual PaymentTypeEntity PaymentType { get; set; }

    public virtual OrderPaymentEntity Payment { get; set; }

    public OrderPaymentTypeEntity() : base() { }
}