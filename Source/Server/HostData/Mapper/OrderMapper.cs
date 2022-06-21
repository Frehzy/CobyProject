using HostData.Domain.Contracts.Models;
using Shared.Factory.Dto;

namespace HostData.Mapper;

public static class OrderMapper
{
    public static OrderDto CreateDto(OrderModel orderModel) =>
        new(orderModel.Number,
            orderModel.Id,
            orderModel.Tables.Select(x => new TableDto(x.Id, x.Name, x.Number, x.IsDeleted)).ToList(),
            new WaiterDto(orderModel.Waiter.Id, orderModel.Waiter.Name, orderModel.Waiter.IsSessionOpen, orderModel.Waiter.Permissions, orderModel.Waiter.IsDeleted),
            orderModel.StartTime,
            orderModel.CloseTime,
            orderModel.Guests.Select(x => new GuestDto(x.Id, x.Name, x.Rank, x.IsDeleted)).ToList(),
            orderModel.Products.Select(x => ProductMapper.CreateDto(x)).ToList(),
            orderModel.Discounts.Select(x => DiscountMapper.CreateDto(x)).ToList(),
            orderModel.Payments.Select(x => PaymentMapper.CreateDto(x)).ToList(),
            orderModel.Status,
            default,
            default,
            default,
            default,
            orderModel.Version,
            orderModel.IsDeleted);
}