namespace HostData.Domain.Contracts.Entities;

public class WaiterEntity : BaseEntity
{
    public string Name { get; set; }

    public string Password { get; set; }

    public virtual ICollection<PermissionEntity> Permissions { get; set; }

    public virtual OrderEntity Order { get; set; }
}