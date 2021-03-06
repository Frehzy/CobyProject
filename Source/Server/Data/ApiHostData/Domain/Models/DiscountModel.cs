using SharedData.Entities.Implementation;

namespace ApiHostData.Domain.Models;

public class DiscountModel : BaseModel
{
    public DiscountTypeModel Discount { get; set; }

    public decimal DiscountSum { get; set; }

    public bool IsActive { get; set; }

    public DiscountModel() : base() { }
}