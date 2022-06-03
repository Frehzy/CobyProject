using Api.Data.Order;

namespace Api.InternalModel;

internal class Order : IOrder
{
    public Guid OrderId { get; set; }

    public Guid TableId { get; set; }

    public Guid WaiterId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public bool IsDeleted { get; set; }

    public Order() { }
}