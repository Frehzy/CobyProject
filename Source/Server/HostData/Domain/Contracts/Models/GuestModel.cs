namespace HostData.Domain.Contracts.Models;

public class GuestModel : BaseModel
{
    public string Name { get; set; }

    public int Rank { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public bool IsDeleted { get; set; } = false;

    public GuestModel() { }
}