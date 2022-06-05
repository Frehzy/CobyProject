using Api.Data.Order;
using Api.Factory.Dto;
using Api.Factory.InternalModel;

namespace Api.Factory;

internal static class OrderFactory
{
    public static Order Create(IOrder order) =>
        new(order.Id,
            order.TableId,
            order.WaiterId,
            order.StartTime,
            order.EndTime,
            order.OrderStatus,
            order.Version,
            order.Guests?.Select(x => GuestFactory.Create(x)).ToList(),
            order.IsDeleted);

    public static Order Create(OrderDto order) =>
        new(order.Id,
            order.TableId,
            order.WaiterId,
            order.StartTime,
            order.EndTime,
            order.OrderStatus,
            order.Version,
            order.Guests?.Select(x => GuestFactory.Create(x)).ToList(),
            order.IsDeleted);

    public static OrderDto CreateDto(IOrder order) =>
        new(order.Id,
            order.TableId,
            order.WaiterId,
            order.StartTime,
            order.EndTime,
            order.OrderStatus,
            order.Version,
            order.Guests?.Select(x => GuestFactory.Create(x)).ToList(),
            order.IsDeleted);
}