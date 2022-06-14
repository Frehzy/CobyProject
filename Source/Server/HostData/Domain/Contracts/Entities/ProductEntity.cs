using Shared.Data.Enum;

namespace HostData.Domain.Contracts.Entities;

public class ProductEntity : BaseEntity
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public ProductType Type { get; set; }

    public virtual OrderEntity Order { get; set; }
}