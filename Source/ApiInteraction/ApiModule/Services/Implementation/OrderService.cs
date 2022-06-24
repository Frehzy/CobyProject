using ApiModule.Services;
using ApiModule.Services.Contrancts;
using Microsoft.AspNetCore.SignalR.Client;
using Shared.Data.Enum;
using Shared.Factory.Dto;

namespace ApiModule.Services.Implementation;

internal class OrderService : BaseService<OrderDto>, IOrderService
{
    public OrderService(Uri url) : base(new Uri(url, "ordersNotification"))
    {
        Connection.On<OrderDto, EventType>("OnOrder", (dto, eventType) => RaiseReceiveEvent(dto, eventType)); ;
    }

    public async Task SendOrder(OrderDto order, EventType eventType) =>
        await Send(nameof(SendOrder), order, eventType);
}