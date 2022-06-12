using Shared.Data.Enum;

namespace Shared.Factory.Dto;

internal record PaymentDto(Guid Id, decimal Sum, PaymentTypeDto Type, PaymentStatus Status, bool IsDeleted);