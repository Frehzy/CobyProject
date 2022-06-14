namespace HostData.Domain.Contracts.Entities;

public class DiscountEntity : BaseEntity
{
    public string Name { get; set; }

    public decimal DiscountSum { get; set; }

    public virtual OrderEntity Order { get; set; }
}