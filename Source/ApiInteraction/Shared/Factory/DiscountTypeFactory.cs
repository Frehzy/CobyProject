using Shared.Data;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Shared.Factory;

internal class DiscountTypeFactory
{
    public static DiscountType Create(IDiscountType discount) =>
        new(discount.Id, discount.Name, discount.IsDeleted);

    public static DiscountTypeDto CreateDto(IDiscountType discount) =>
        new(discount.Id, discount.Name, discount.IsDeleted);

    public static DiscountType Create(DiscountTypeDto discount) =>
        new(discount.Id, discount.Name, discount.IsDeleted);
}