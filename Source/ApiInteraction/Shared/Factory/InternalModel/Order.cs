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

    public IReadOnlyList<IDiscount> Discounts { get; set; }

    public IReadOnlyList<IPayment> Payments { get; set; }

    public OrderStatus Status { get; set; }

    public int Version { get; set; }

    public bool IsDeleted { get; set; }

    public Order() { }

    public Order(int number, Guid id, IReadOnlyList<ITable> table, IWaiter waiter)
    {
        Number = number;
        Id = id;
        Tables = table;
        Waiter = waiter;
        StartTime = DateTime.Now;
        Guests = new List<IGuest>();
        Products = new List<IProduct>();
        Discounts = new List<IDiscount>();
        Payments = new List<IPayment>();
        Status = OrderStatus.Open;
        Version = 1;
        IsDeleted = false;
    }

    public Order(int number, Guid id, IReadOnlyList<ITable> tables, IWaiter waiter, DateTime startTime, DateTime? endTime, IReadOnlyList<IGuest>? guests, IReadOnlyList<IProduct>? products, IReadOnlyList<IDiscount> discounts, IReadOnlyList<IPayment> payments, OrderStatus status, int version, bool isDeleted)
    {
        Number = number;
        Id = id;
        Tables = tables;
        Waiter = waiter;
        StartTime = startTime;
        EndTime = endTime;
        Guests = guests;
        Products = products;
        Discounts = discounts;
        Payments = payments;
        Status = status;
        Version = version;
        IsDeleted = isDeleted;
    }

    public IReadOnlyList<IDiscount> GetDiscounts() =>
        (Discounts ?? Enumerable.Empty<IDiscount>()).ToList();

    public IReadOnlyList<IProduct> GetProducts() =>
        (Products ?? Enumerable.Empty<IProduct>()).ToList();

    public IReadOnlyList<ITable> GetTables() =>
        (Tables ?? Enumerable.Empty<ITable>()).ToList();

    public IReadOnlyList<IGuest> GetGuests() =>
        (Guests ?? Enumerable.Empty<IGuest>()).ToList();

    public IReadOnlyList<IPayment> GetPayments() =>
        (Payments ?? Enumerable.Empty<IPayment>()).ToList();
}