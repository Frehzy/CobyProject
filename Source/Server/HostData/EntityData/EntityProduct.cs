using Shared.Data.Enum;

namespace HostData.EntityData;

public class EntityProduct
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public ProductType Type { get; set; }

    public bool IsDeleted { get; set; }
}