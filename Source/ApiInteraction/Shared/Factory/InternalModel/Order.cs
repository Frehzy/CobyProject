using Shared.Data;
using Shared.Data.Enum;

namespace Shared.Factory.InternalModel;

internal class Order : IOrder
{
    public Guid Id { get; set; }

    public IReadOnlyList<ITable> Tables { get; set; }

    public IWaiter Waiter { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public OrderStatus Status { get; set; }

    public bool IsDeleted { get; set; }

    public int Version { get; set; }

    public IReadOnlyList<IGuest> Guests { get; set; }

    public Order() { }

    public Order(Guid orderId, Table table, Waiter waiter, DateTime startTime, DateTime? endTime, OrderStatus orderStatus, int version, List<Guest> guests = null, bool isDeleted = false)
    {
        Id = orderId;
        Tables = new List<Table>() { table } ;
        Waiter = waiter;
        StartTime = startTime;
        EndTime = endTime;
        Status = orderStatus;
        Version = version;
        Guests = guests;
        IsDeleted = isDeleted;
    }

    public Order(Guid orderId, List<Table> tables, Waiter waiter, DateTime startTime, DateTime? endTime, OrderStatus orderStatus, int version, List<Guest> guests = null, bool isDeleted = false)
    {
        Id = orderId;
        Tables = tables;
        Waiter = waiter;
        StartTime = startTime;
        EndTime = endTime;
        Status = orderStatus;
        Version = version;
        Guests = guests;
        IsDeleted = isDeleted;
    }

    public IReadOnlyList<ITable> GetTables() =>
        (Tables ?? Enumerable.Empty<ITable>()).ToList();

    public IReadOnlyList<IGuest> GetGuests() =>
        (Guests ?? Enumerable.Empty<IGuest>()).ToList();
}