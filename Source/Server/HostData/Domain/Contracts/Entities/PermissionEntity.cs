using Shared.Data.Enum;

namespace HostData.Domain.Contracts.Entities;

public class PermissionEntity : BaseEntity
{
    public EmployeePermission EmployeePermission { get; set; }

    public virtual WaiterEntity Waiter { get; set; }
}