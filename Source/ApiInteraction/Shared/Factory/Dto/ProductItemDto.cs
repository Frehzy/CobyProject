using Shared.Data.Enum;

namespace Shared.Factory.Dto;

public record ProductItemDto(Guid Id, string Name, decimal Price, ProductType Type);