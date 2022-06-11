using Shared.Data.Enum;

namespace Shared.Factory.Dto;

internal record OrderDto(Guid Id,
                         List<TableDto> Tables,
                         WaiterDto Waiter,
                         DateTime StartTime,
                         DateTime? EndTime,
                         OrderStatus Status,
                         int Version,
                         List<GuestDto> Guests = default,
                         bool IsDeleted = false)
{
    public List<TableDto> GetTables() =>
        (Tables ?? Enumerable.Empty<TableDto>()).ToList();

    public List<GuestDto> GetGuests() => (Guests ?? Enumerable.Empty<GuestDto>()).ToList();
}