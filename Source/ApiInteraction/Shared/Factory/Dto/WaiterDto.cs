﻿using Shared.Data.Enum;

namespace Shared.Factory.Dto;

internal record WaiterDto(Guid Id, string Name, bool IsSessionOpen, List<EmployeePermission> Permissions)
{
    public List<EmployeePermission> GetPermissions() => (Permissions ?? Enumerable.Empty<EmployeePermission>()).ToList();
}