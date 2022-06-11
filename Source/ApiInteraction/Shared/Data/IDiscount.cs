namespace Shared.Data;

public interface IDiscount
{
    public Guid Id { get; }

    public string Name { get; }

    public decimal DiscountSum { get; }

    public bool IsActive { get; }

    public bool IsDeleted { get; }
}