using Api.Data.Order;
using Api.Factory.InternalModel;

namespace Api.Factory.Dto;

internal class OrderDto
{
    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public Guid WaiterId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public bool IsDeleted { get; set; }

    public int Version { get; set; }

    public List<Guest> Guests { get; set; }

    public OrderDto() { }

    public OrderDto(Guid orderId, Guid tableId, Guid waiterId, DateTime startTime, DateTime? endTime, OrderStatus orderStatus, int version, List<Guest> guests = null, bool isDeleted = false)
    {
        Id = orderId;
        TableId = tableId;
        WaiterId = waiterId;
        StartTime = startTime;
        EndTime = endTime;
        OrderStatus = orderStatus;
        Version = version;
        Guests = guests;
        IsDeleted = isDeleted;
    }
}
