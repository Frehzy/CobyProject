using Shared.Data.Enum;

namespace Shared.Data;

public interface IProductItem
{
    public Guid Id { get; }

    public string Name { get; }

    public decimal Price { get; }

    public ProductType Type { get; }
}