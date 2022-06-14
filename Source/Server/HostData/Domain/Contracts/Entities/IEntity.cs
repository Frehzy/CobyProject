namespace HostData.Domain.Contracts.Entities;

public interface IEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedTime { get; set; }

    public Guid WaiterCreatedId { get; set; }

    public DateTime? UpdateTime { get; set; }

    public Guid WaiterUpdatedId { get; set; }

    public bool IsActive { get; set; }
}