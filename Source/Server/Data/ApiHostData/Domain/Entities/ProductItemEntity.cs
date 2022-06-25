using Shared.Data.Enum;
using SharedData.Entities.Implementation;

namespace ApiHostData.Domain.Entities;

public class ProductItemEntity : BaseEntity
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public ProductType Type { get; set; }
}