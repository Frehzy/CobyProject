using Shared.Data;

namespace Shared.Factory.InternalModel;

internal class DiscountType : IDiscountType
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DiscountType() { }

    public DiscountType(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}