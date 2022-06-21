using Shared.Data.Enum;

namespace Shared.Factory.Dto;

public record PaymentTypeDto(Guid Id, string Name, PaymentTypeKind Kind, bool NeedOpenCashBox);