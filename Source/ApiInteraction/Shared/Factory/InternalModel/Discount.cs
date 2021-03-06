using Shared.Data;

namespace Shared.Factory.InternalModel;

internal class Discount : IDiscount
{
    public Guid Id { get; set; }

    public IDiscountType Type { get; set; }

    public decimal DiscountSum { get; set; }

    public bool IsActive { get; set; }

    public Discount() { }

    public Discount(Guid id, IDiscountType type, decimal discountSum, bool isActive)
    {
        Id = id;
        Type = type;
        DiscountSum = discountSum;
        IsActive = isActive;
    }
}
