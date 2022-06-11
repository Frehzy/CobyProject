using Shared.Data;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Shared.Factory;

internal static class OrderFactory
{
    public static Order Create(IOrder order) =>
        new(order.Id,
            order.TableId,
            WaiterFactory.Create(order.Waiter),
            order.StartTime,
            order.EndTime,
            order.Status,
            order.Version,
            order.GetGuests().Select(x => GuestFactory.Create(x)).ToList(),
            order.IsDeleted);

    public static Order Create(OrderDto order) =>
        new(order.Id,
            order.TableId,
            WaiterFactory.Create(order.Waiter),
            order.StartTime,
            order.EndTime,
            order.Status,
            order.Version,
            order.GetGuests().Select(x => GuestFactory.Create(x)).ToList(),
            order.IsDeleted);

    public static OrderDto CreateDto(IOrder order) =>
        new(order.Id,
            order.TableId,
            WaiterFactory.CreateDto(order.Waiter),
            order.StartTime,
            order.EndTime,
            order.Status,
            order.Version,
            order.GetGuests().Select(x => GuestFactory.CreateDto(x)).ToList(),
            order.IsDeleted);
}