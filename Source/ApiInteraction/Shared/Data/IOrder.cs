using Shared.Data.Enum;

namespace Shared.Data;

public interface IOrder
{
    public Guid Id { get; }

    public Guid TableId { get; }

    public Guid WaiterId { get; }

    public DateTime StartTime { get; }

    public DateTime? EndTime { get; }

    public IReadOnlyList<IGuest> Guests { get; }

    public OrderStatus OrderStatus { get; }

    public int Version { get; }

    public bool IsDeleted { get; }

    public IReadOnlyList<IGuest> GetGuests();
}