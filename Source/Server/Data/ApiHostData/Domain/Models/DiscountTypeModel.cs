using SharedData.Entities.Implementation;

namespace ApiHostData.Domain.Models;

public class DiscountTypeModel : BaseModel
{
    public string Name { get; set; }

    public DiscountTypeModel() : base() { }
}