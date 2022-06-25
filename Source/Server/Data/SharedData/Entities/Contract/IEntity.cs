namespace SharedData.Entities.Contract;

public interface IEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedTime { get; set; }

    public Guid WaiterCreatedId { get; set; }

    public DateTime? UpdateTime { get; set; }

    public Guid WaiterUpdatedId { get; set; }

    public int Version { get; set; }

    public bool IsDeleted { get; set; }
}