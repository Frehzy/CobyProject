namespace Shared.Factory.Dto;

internal record DiscountDto(Guid Id, string Name, decimal DiscountSum, bool IsActive, bool IsDeleted);