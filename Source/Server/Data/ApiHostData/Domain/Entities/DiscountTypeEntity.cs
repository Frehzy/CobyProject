using SharedData.Entities.Implementation;

namespace ApiHostData.Domain.Entities;

public class DiscountTypeEntity : BaseEntity
{
    public string Name { get; set; }

    public DiscountTypeEntity() { }
}