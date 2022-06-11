﻿using Shared.Data.Enum;

namespace Shared.Factory.Dto;

internal record ProductDto(Guid Id,
                           string Name,
                           decimal Price,
                           Guid GuestId,
                           Guid WaiterId,
                           DateTime? PrintTime,
                           ProductStatus Status,
                           ProductType Type,
                           bool IsDeleted,
                           string? Comment);