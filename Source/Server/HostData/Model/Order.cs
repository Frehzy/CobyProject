using Api.Data.Order;

namespace HostData.Model;

public class Order : IOrder
{
    public Guid OrderId { get; set; }

    public Guid TableId { get; set; }

    public Guid WaiterId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public bool IsDeleted { get; set; }

    public Order() { }

    public Order(Guid orderId, Guid tableId, Guid waiterId, DateTime startTime, DateTime? endTime, OrderStatus orderStatus, bool isDeleted)
    {
        OrderId = orderId;
        TableId = tableId;
        WaiterId = waiterId;
        StartTime = startTime;
        EndTime = endTime;
        OrderStatus = orderStatus;
        IsDeleted = isDeleted;
    }
}