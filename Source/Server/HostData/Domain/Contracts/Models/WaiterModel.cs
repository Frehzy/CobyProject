using Shared.Data.Enum;

namespace HostData.Domain.Contracts.Models;

public class WaiterModel : BaseModel
{
    public string Name { get; set; }

    public string Password { get; set; }

    public bool IsSessionOpen { get; set; }

    public List<EmployeePermission> Permissions { get; set; }

    public WaiterModel() : base() { }
}