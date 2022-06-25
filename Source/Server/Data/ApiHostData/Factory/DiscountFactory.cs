using ApiHostData.Domain.Models;
using Shared.Factory.Dto;

namespace ApiHostData.Factory;

public static class DiscountFactory
{
    public static DiscountDto CreateDto(DiscountModel discountModel) =>
        new(discountModel.Id,
            CreateDto(discountModel.Discount),
            discountModel.DiscountSum,
            discountModel.IsActive);

    public static DiscountTypeDto CreateDto(DiscountTypeModel discountTypeModel) =>
        new(discountTypeModel.Id,
            discountTypeModel.Name);
}