using Shared.Data;
using Shared.Data.Enum;

namespace Shared.Factory.InternalModel;

internal class Order : IOrder
{
    public int Number { get; set; }

    public Guid Id { get; set; }

    public IReadOnlyList<ITable> Tables { get; set; }

    public IWaiter Waiter { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public IReadOnlyList<IGuest>? Guests { get; set; }

    public IReadOnlyList<IProduct>? Products { get; set; }

    public OrderStatus Status { get; set; }

    public int Version { get; set; }

    public bool IsDeleted { get; set; }

    public Order() { }

    public Order(int number, Guid id, ITable table, IWaiter waiter)
    {
        Number = number;
        Id = id;
        Tables = new List<ITable>() { table };
        Waiter = waiter;
        StartTime = DateTime.Now;
        Guests = new List<IGuest>();
        Products = new List<IProduct>();
        Status = OrderStatus.Open;
        Version = 1;
        IsDeleted = false;
    }

    public Order(int number, Guid id, IReadOnlyList<ITable> tables, IWaiter waiter, DateTime startTime, DateTime? endTime, IReadOnlyList<IGuest>? guests, IReadOnlyList<IProduct>? products, OrderStatus status, int version, bool isDeleted)
    {
        Number = number;
        Id = id;
        Tables = tables;
        Waiter = waiter;
        StartTime = startTime;
        EndTime = endTime;
        Guests = guests;
        Products = products;
        Status = status;
        Version = version;
        IsDeleted = isDeleted;
    }

    public IReadOnlyList<IProduct> GetProducts() =>
        (Products ?? Enumerable.Empty<IProduct>()).ToList();

    public IReadOnlyList<ITable> GetTables() =>
        (Tables ?? Enumerable.Empty<ITable>()).ToList();

    public IReadOnlyList<IGuest> GetGuests() =>
        (Guests ?? Enumerable.Empty<IGuest>()).ToList();
}