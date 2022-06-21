using Shared.Data;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Shared.Factory;

internal static class OrderFactory
{
    public static Order Create(IOrder order) =>
        new(order.Number,
            order.Id,
            order.GetTables().Select(x => TableFactory.Create(x)).ToList(),
            WaiterFactory.Create(order.Waiter),
            order.StartTime,
            order.CloseTime,
            order.GetGuests().Select(x => GuestFactory.Create(x)).ToList(),
            order.GetProducts().Select(x => ProductFactory.Create(x)).ToList(),
            order.GetDiscounts().Select(x => DiscountFactory.Create(x)).ToList(),
            order.GetPayments().Select(x => PaymentFactory.Create(x)).ToList(),
            order.Status,
            order.Version);

    public static Order Create(OrderDto order) =>
        new(order.Number,
            order.Id,
            order.GetTables().Select(x => TableFactory.Create(x)).ToList(),
            WaiterFactory.Create(order.Waiter),
            order.StartTime,
            order.CloseTime,
            order.GetGuests().Select(x => GuestFactory.Create(x)).ToList(),
            order.GetProducts().Select(x => ProductFactory.Create(x)).ToList(),
            order.GetDiscounts().Select(x => DiscountFactory.Create(x)).ToList(),
            order.GetPayments().Select(x => PaymentFactory.Create(x)).ToList(),
            order.Status,
            order.Version);

    public static OrderDto CreateDto(IOrder order) =>
        new(order.Number,
            order.Id,
            order.GetTables().Select(x => TableFactory.CreateDto(x)).ToList(),
            WaiterFactory.CreateDto(order.Waiter),
            order.StartTime,
            order.CloseTime,
            order.GetGuests().Select(x => GuestFactory.CreateDto(x)).ToList(),
            order.GetProducts().Select(x => ProductFactory.CreateDto(x)).ToList(),
            order.GetDiscounts().Select(x => DiscountFactory.CreateDto(x)).ToList(),
            order.GetPayments().Select(x => PaymentFactory.CreateDto(x)).ToList(),
            order.Status,
            order.TotalSum,
            order.ResultSum,
            order.DiscountsSum,
            order.PaymentsSum,
            order.Version);
}