﻿using Shared.Data.Enum;

namespace Shared.Factory.Dto;

internal record PaymentTypeDto(Guid Id, string Name, PaymentTypeKind Kind, bool NeedOpenCashBox, bool IsDeleted);