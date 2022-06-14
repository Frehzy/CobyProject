namespace HostData.Domain.Contracts.Entities;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public Guid WaiterCreatedId { get; set; }

    public DateTime? UpdateTime { get; set; }

    public Guid WaiterUpdatedId { get; set; }

    public bool IsActive { get; set; } = false;
}