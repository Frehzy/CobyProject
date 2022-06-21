namespace Shared.Factory.Dto;

public record DiscountDto(Guid Id, DiscountTypeDto Type, decimal DiscountSum, bool IsActive);