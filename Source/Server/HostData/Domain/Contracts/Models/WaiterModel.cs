namespace HostData.Domain.Contracts.Models;

public class WaiterModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public Guid PermissionId { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public bool IsDeleted { get; set; } = false;

    public WaiterModel() { }
}