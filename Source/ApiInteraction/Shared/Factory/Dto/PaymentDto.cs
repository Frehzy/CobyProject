using Shared.Data.Enum;

namespace Shared.Factory.Dto;

public record PaymentDto(Guid Id, decimal Sum, PaymentTypeDto Type, PaymentStatus Status, bool IsDeleted);