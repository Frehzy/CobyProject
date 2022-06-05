using Shared.Data;
using Shared.Data.Enum;

namespace Shared.Factory.InternalModel;

internal class Order : IOrder
{
    public Guid Id { get; set; }

    public Guid TableId { get; set; }

    public Guid WaiterId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public bool IsDeleted { get; set; }

    public int Version { get; set; }

    public IReadOnlyList<IGuest> Guests { get; set; }

    public Order() { }

    public Order(Guid orderId, Guid tableId, Guid waiterId, DateTime startTime, DateTime? endTime, OrderStatus orderStatus, int version, List<Guest> guests = null, bool isDeleted = false)
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

    public IReadOnlyList<IGuest> GetGuests() =>
        (Guests ?? Enumerable.Empty<IGuest>()).ToList();
}