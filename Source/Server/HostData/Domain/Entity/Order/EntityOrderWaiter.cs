namespace HostData.EntityData.Order;

public class EntityOrderWaiter
{
    public int OperationNumber { get; set; }

    public Guid OrderId { get; set; }

    public Guid WaiterId { get; set; }
}