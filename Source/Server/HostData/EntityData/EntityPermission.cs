using Shared.Data.Enum;

namespace HostData.EntityData;

public class EntityPermission
{
    public EmployeePermission EmployeePermission { get; set; }

    public Guid WaiterId { get; set; }
}