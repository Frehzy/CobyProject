using HostData.Domain.Contracts.Models;
using Shared.Factory.Dto;

namespace HostData.Factory;

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