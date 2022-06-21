using Shared.Data.Enum;

namespace Shared.Factory.Dto;

public record ProductDto(Guid Id,
                        Guid GuestId,
                        Guid WaiterId,
                        DateTime? PrintTime,
                        ProductStatus Status,
                        ProductItemDto ProductItem,
                        string? Comment);