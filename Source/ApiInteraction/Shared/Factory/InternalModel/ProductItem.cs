using Shared.Data;
using Shared.Data.Enum;

namespace Shared.Factory.InternalModel;

internal class ProductItem : IProductItem
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public ProductType Type { get; set; }

    public ProductItem() { }

    public ProductItem(Guid id, string name, decimal price, ProductType type)
    {
        Id = id;
        Name = name;
        Price = price;
        Type = type;
    }
}