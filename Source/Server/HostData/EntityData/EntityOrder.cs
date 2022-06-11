using Shared.Data.Enum;

namespace HostData.EntityData;

public class EntityOrder
{
    public int Number { get; set; }

    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public Guid WaiterId { get; set; }

    public Guid DiscountId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public OrderStatus Status { get; set; }

    public bool Version { get; set; }
}