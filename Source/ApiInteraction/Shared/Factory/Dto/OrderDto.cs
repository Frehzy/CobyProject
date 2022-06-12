using Shared.Data.Enum;

namespace Shared.Factory.Dto;

internal record OrderDto(int Number,
                         Guid Id,
                         List<TableDto> Tables,
                         WaiterDto Waiter,
                         DateTime StartTime,
                         DateTime? EndTime,
                         List<GuestDto> Guests,
                         List<ProductDto> Products,
                         List<DiscountDto> Discounts,
                         List<PaymentDto> Payments,
                         OrderStatus Status,
                         int Version,
                         bool IsDeleted)
{
    public List<ProductDto> GetProducts() =>
        (Products ?? Enumerable.Empty<ProductDto>()).ToList();

    public List<PaymentDto> GetPayments() =>
        (Payments ?? Enumerable.Empty<PaymentDto>()).ToList();

    public List<DiscountDto> GetDiscounts() =>
        (Discounts ?? Enumerable.Empty<DiscountDto>()).ToList();

    public List<TableDto> GetTables() =>
        (Tables ?? Enumerable.Empty<TableDto>()).ToList();

    public List<GuestDto> GetGuests() =>
        (Guests ?? Enumerable.Empty<GuestDto>()).ToList();
}