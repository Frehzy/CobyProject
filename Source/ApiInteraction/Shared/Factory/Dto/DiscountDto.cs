namespace Shared.Factory.Dto;

public record DiscountDto(Guid Id, string Name, decimal DiscountSum, bool IsActive, bool IsDeleted);