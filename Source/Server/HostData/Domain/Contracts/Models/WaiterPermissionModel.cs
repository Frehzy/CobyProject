namespace HostData.Domain.Contracts.Models;

public class WaiterPermissionModel
{
    public Guid Id { get; set; } //guid waiter

    public WaiterModel Waiter { get; set; }

    public List<PermissionModel> Permissions { get; set; }
}