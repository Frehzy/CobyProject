using Shared.Data.Enum;

namespace Shared.Factory.Dto;

public record WaiterDto(Guid Id, string Name, bool IsSessionOpen, List<EmployeePermission> Permissions)
{
    public List<EmployeePermission> GetPermissions() =>
        (Permissions ?? Enumerable.Empty<EmployeePermission>()).ToList();
}