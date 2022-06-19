using Shared.Data.Enum;

namespace HostData.Domain.Contracts.Entities;

public class ProductItemEntity : BaseEntity
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public ProductType Type { get; set; }
}