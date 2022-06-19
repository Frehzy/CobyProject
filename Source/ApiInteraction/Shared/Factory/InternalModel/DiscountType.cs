using Shared.Data;

namespace Shared.Factory.InternalModel;

internal class DiscountType : IDiscountType
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsDeleted { get; set; }

    public DiscountType() { }

    public DiscountType(Guid id, string name, bool isDeleted)
    {
        Id = id;
        Name = name;
        IsDeleted = isDeleted;
    }
}