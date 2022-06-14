using Shared.Data.Enum;

namespace HostData.Domain.Contracts.Models;

public class PermissionModel
{
    public Guid Id { get; set; }

    public EmployeePermission EmployeePermission { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.Now;
}