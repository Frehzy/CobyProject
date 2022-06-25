using SharedData.Entities.Contract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharedData.Entities.Implementation;

public abstract class BaseEntity : IEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public Guid WaiterCreatedId { get; set; } = Guid.Empty;

    public DateTime? UpdateTime { get; set; }

    public Guid WaiterUpdatedId { get; set; }

    public int Version { get; set; } = 1;

    [DefaultValue("false")]
    public bool IsDeleted { get; set; } = false;

    public BaseEntity() { }
}