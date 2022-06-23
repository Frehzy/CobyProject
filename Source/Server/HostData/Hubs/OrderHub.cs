using HostData.Domain.Contracts.Services;
using HostData.Factory;
using Microsoft.AspNetCore.SignalR;
using Shared.Factory.Dto;

namespace HostData.Hubs;

public class OrderHub : Hub
{
    private readonly IOrderService _orderService;

    public OrderHub(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public override async Task OnConnectedAsync()
    {
        foreach(var order in await _orderService.Get())
            await Clients.Client(Context.ConnectionId).SendAsync("OnOrder", OrderFactory.CreateDto(order));

        await base.OnConnectedAsync();
    }

    public async Task SendOrder(OrderDto order)
    {
        await Clients.All.SendAsync("OnOrder", order);
    }
}