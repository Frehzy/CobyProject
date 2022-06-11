using Shared.Data.Enum;

namespace Shared.Factory.Dto;

internal record OrderDto(Guid Id,
                         Guid TableId,
                         WaiterDto Waiter,
                         DateTime StartTime,
                         DateTime? EndTime,
                         OrderStatus Status,
                         int Version,
                         List<GuestDto> Guests = default,
                         bool IsDeleted = false)
{
    public List<GuestDto> GetGuests() => (Guests ?? Enumerable.Empty<GuestDto>()).ToList();
}