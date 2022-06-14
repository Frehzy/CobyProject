namespace HostData.EntityData.Order;

public class EntityOrderGuest
{
    public int OperationNumber { get; set; }

    public Guid OrderId { get; set; }

    public Guid GuestId { get; set; }

    public string Name { get; }

    public int Rank { get; }

    public bool IsDeleted { get; }
}