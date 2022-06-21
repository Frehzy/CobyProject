using HostData.Domain.Contracts.Models;
using Shared.Factory.Dto;

namespace HostData.Factory;

public static class OrderFactory
{
    public static OrderDto CreateDto(OrderModel orderModel) =>
        new(orderModel.Number,
            orderModel.Id,
            orderModel.Tables.Select(x => TableFactory.CreateDto(x)).ToList(),
            WaiterFactory.CreateDto(orderModel.Waiter),
            orderModel.StartTime,
            orderModel.CloseTime,
            orderModel.Guests.Select(x => new GuestDto(x.Id, x.Name, x.Rank)).ToList(),
            orderModel.Products.Select(x => ProductFactory.CreateDto(x)).ToList(),
            orderModel.Discounts.Select(x => DiscountFactory.CreateDto(x)).ToList(),
            orderModel.Payments.Select(x => PaymentFactory.CreateDto(x)).ToList(),
            orderModel.Status,
            default,
            default,
            default,
            default,
            orderModel.Version);
}