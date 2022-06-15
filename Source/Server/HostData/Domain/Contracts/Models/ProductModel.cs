using Shared.Data.Enum;

namespace HostData.Domain.Contracts.Models;

public class ProductModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public ProductType Type { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public bool IsDeleted { get; set; } = false;

    public ProductModel() { }
}