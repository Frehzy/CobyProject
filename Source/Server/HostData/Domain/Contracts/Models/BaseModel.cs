namespace HostData.Domain.Contracts.Models;

public class BaseModel : IModel
{
    public Guid Id { get; set; }

    public BaseModel() { }
}