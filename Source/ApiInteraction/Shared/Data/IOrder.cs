using Shared.Data.Enum;

namespace Shared.Data;

public interface IOrder
{
    public int Number { get; }

    public Guid Id { get; }

    public IReadOnlyList<ITable> Tables { get; }

    public IWaiter Waiter { get; }

    public DateTime StartTime { get; }

    public DateTime? CloseTime { get; }

    public IReadOnlyList<IGuest> Guests { get; }

    public IReadOnlyList<IProduct> Products { get; }

    public IReadOnlyList<IDiscount> Discounts { get; }

    public IReadOnlyList<IPayment> Payments { get; }

    public OrderStatus Status { get; }

    public decimal TotalSum { get; } //сумма без скидки

    public decimal ResultSum { get; } //сумма со скидкой

    public decimal DiscountsSum { get; } //сумма скидок

    public decimal PaymentsSum { get; } //сумма всех New оплат

    public int Version { get; }

    public bool IsDeleted { get; }

    public IReadOnlyList<ITable> GetTables();

    public IReadOnlyList<IDiscount> GetDiscounts();

    public IReadOnlyList<IGuest> GetGuests();

    public IReadOnlyList<IProduct> GetProducts();

    public IReadOnlyList<IPayment> GetPayments();
}