namespace Api.Data.Order;

public interface IOrder
{
    public Guid OrderId { get; }

    public Guid TableId { get; }

    public Guid WaiterId { get; }

    public DateTime StartTime { get; }

    public DateTime? EndTime { get; }

    public OrderStatus OrderStatus { get; }

    public bool IsDeleted { get; }
}