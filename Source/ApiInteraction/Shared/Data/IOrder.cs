using Shared.Data.Enum;

namespace Shared.Data;

public interface IOrder
{
    public int Number { get; }

    public Guid Id { get; }

    public IReadOnlyList<ITable> Tables { get; }

    public IWaiter Waiter { get; }

    public DateTime StartTime { get; }

    public DateTime? EndTime { get; }

    public IReadOnlyList<IGuest> Guests { get; }

    public IReadOnlyList<IProduct> Products { get; }

    public IReadOnlyList<IDiscount> Discounts { get; }

    public IReadOnlyList<IPayment> Payments { get; }

    public OrderStatus Status { get; }

    public int Version { get; }

    public bool IsDeleted { get; }

    public IReadOnlyList<ITable> GetTables();

    public IReadOnlyList<IDiscount> GetDiscounts();

    public IReadOnlyList<IGuest> GetGuests();

    public IReadOnlyList<IProduct> GetProducts();

    public IReadOnlyList<IPayment> GetPayments();
}