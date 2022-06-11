﻿using Shared.Data;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Shared.Factory;

internal class DiscountFactory
{
    public static Discount Create(IDiscount discount) =>
        new(discount.Id, discount.Name, discount.DiscountSum, discount.IsActive, discount.IsDeleted);

    public static DiscountDto CreateDto(IDiscount discount) =>
        new(discount.Id, discount.Name, discount.DiscountSum, discount.IsActive, discount.IsDeleted);

    public static Discount Create(DiscountDto discount) =>
        new(discount.Id, discount.Name, discount.DiscountSum, discount.IsActive, discount.IsDeleted);
}