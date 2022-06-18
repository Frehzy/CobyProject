using Shared.Data.Enum;

namespace HostData.Domain.Contracts.Models;

public class WaiterModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public string Password { get; set; }

    public bool IsSessionOpen { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public List<EmployeePermission> Permissions { get; set; }

    public bool IsDeleted { get; set; } = false;

    public WaiterModel() { }
}