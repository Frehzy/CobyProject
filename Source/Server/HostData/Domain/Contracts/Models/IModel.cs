namespace HostData.Domain.Contracts.Models;

public interface IModel
{
    public Guid Id { get; set; }

    public DateTime CreatedTime { get; set; }

    public bool IsDeleted { get; set; }
}