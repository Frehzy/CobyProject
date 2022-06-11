using Shared.Data;

namespace Shared.Factory.InternalModel;

internal class Discount : IDiscount
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal DiscountSum { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Discount() { }

    public Discount(Guid id, string name, decimal discountSum, bool isActive = true, bool isDeleted = false)
    {
        Id = id;
        Name = name;
        DiscountSum = discountSum;
        IsActive = isActive;
        IsDeleted = isDeleted;
    }
}
