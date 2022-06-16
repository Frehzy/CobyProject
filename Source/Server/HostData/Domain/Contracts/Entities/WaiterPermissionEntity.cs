using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HostData.Domain.Contracts.Entities;

public class WaiterPermissionEntity : BaseEntity
{
    public virtual List<Guid> PermissionsId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(PermissionsId))]
    public virtual ICollection<PermissionEntity> Permissions { get; set; }
}