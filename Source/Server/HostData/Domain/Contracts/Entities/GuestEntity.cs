namespace HostData.Domain.Contracts.Entities;

public class GuestEntity : BaseEntity
{
    public string Name { get; set; }

    public int Rank { get; set; }

    public GuestEntity() { }
}