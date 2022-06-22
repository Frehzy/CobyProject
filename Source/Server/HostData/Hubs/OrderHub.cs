using Microsoft.AspNetCore.SignalR;
using Shared.Factory.Dto;

namespace HostData.Hubs;

public class OrderHub : Hub
{
    public async Task SendOrder(OrderDto order)
    {
        await Clients.All.SendAsync("OnOrder", order);
    }
}