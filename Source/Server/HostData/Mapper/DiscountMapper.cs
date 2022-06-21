using HostData.Domain.Contracts.Models;
using Shared.Factory.Dto;

namespace HostData.Mapper;

public static class DiscountMapper
{
    public static DiscountDto CreateDto(DiscountModel discountModel) =>
        new(discountModel.Id,
            CreateDto(discountModel.Discount),
            discountModel.DiscountSum,
            discountModel.IsActive,
            discountModel.IsDeleted);

    public static DiscountTypeDto CreateDto(DiscountTypeModel discountTypeModel) =>
        new(discountTypeModel.Id,
            discountTypeModel.Name,
            discountTypeModel.IsDeleted);
}