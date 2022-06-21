using Shared.Data;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Shared.Factory;

internal class DiscountFactory
{
    public static Discount Create(IDiscount discount) =>
        new(discount.Id, DiscountTypeFactory.Create(discount.Type), discount.DiscountSum, discount.IsActive);

    public static DiscountDto CreateDto(IDiscount discount) =>
        new(discount.Id, DiscountTypeFactory.CreateDto(discount.Type), discount.DiscountSum, discount.IsActive);

    public static Discount Create(DiscountDto discount) =>
        new(discount.Id, DiscountTypeFactory.Create(discount.Type), discount.DiscountSum, discount.IsActive);
}