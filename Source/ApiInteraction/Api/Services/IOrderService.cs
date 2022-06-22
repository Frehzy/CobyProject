using Shared.Factory.Dto;

namespace Api.Services;

internal interface IOrderService
{
    public bool IsConnected { get; }

    event Action<OrderDto>? OnOrder;

    Task SendOrder(OrderDto order);

    Task Connect();

    Task Disconnect();
}