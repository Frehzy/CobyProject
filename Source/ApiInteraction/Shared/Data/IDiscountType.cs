namespace Shared.Data;

public interface IDiscountType
{
    public Guid Id { get; }

    public string Name { get; }

    public bool IsDeleted { get; }
}