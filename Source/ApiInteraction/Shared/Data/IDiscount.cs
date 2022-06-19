namespace Shared.Data;

public interface IDiscount
{
    public Guid Id { get; }

    public IDiscountType Type { get; }

    public decimal DiscountSum { get; }

    public bool IsActive { get; }

    public bool IsDeleted { get; }
}