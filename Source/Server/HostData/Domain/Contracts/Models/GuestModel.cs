namespace HostData.Domain.Contracts.Models;

public class GuestModel : BaseModel
{
    public string Name { get; set; }

    public int Rank { get; set; }

    public GuestModel() { }
}