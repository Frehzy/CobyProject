namespace HostData.EntityData.Order;

public class EntityOrderDiscount
{
    public int OperationNumber { get; set; }

    public Guid OrderId { get; set; }

    public Guid DiscountId { get; set; }

    public decimal DiscountSum { get; set; }

    public bool IsDeleted { get; }
}