using Shared.Data.Enum;
using SharedData.Entities.Implementation;

namespace ApiHostData.Domain.Entities;

public class WaiterEntity : BaseEntity
{
    public string Name { get; set; }

    public string Password { get; set; }

    public bool IsSessionOpen { get; set; }

    public List<EmployeePermission> Permissions { get; set; }
}