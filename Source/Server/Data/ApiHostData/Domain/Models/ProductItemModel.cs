using Shared.Data.Enum;
using SharedData.Entities.Implementation;

namespace ApiHostData.Domain.Models;

public class ProductItemModel : BaseModel
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public ProductType Type { get; set; }

    public ProductItemModel() : base() { }
}