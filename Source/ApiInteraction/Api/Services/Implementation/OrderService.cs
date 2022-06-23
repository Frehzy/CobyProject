using Api.Services.Contrancts;
using Microsoft.AspNetCore.SignalR.Client;
using Shared.Factory.Dto;

namespace Api.Services.Implementation;

internal class OrderService : BaseService<OrderDto>, IOrderService
{
    public OrderService(Uri url) : base(new Uri(url, "ordersNotification"))
    {
        Connection.On<OrderDto>("OnOrder", (dto) => RaiseReceiveEvent(dto)); ;
    }

    public async Task SendOrder(OrderDto order) =>
        await Send(nameof(SendOrder), order);
}