namespace HostData.Domain.Contracts.Models;

public class BaseModel : IModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public bool IsDeleted { get; set; } = false;

    public BaseModel() { }
}