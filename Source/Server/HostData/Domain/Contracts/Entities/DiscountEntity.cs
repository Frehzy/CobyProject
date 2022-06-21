namespace HostData.Domain.Contracts.Entities;

public class DiscountEntity : BaseEntity
{
    public decimal DiscountSum { get; set; }

    public bool IsActive { get; set; }

    public DiscountTypeEntity DiscountType { get; set; }
}