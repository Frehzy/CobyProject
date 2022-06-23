using HostData.Domain.Contracts.Services;
using HostData.Factory;
using Microsoft.AspNetCore.SignalR;
using Shared.Factory.Dto;

namespace HostData.Hubs;

public class WaiterHub : Hub
{
    private readonly IWaiterService _waiterService;

    public WaiterHub(IWaiterService waiterService)
    {
        _waiterService = waiterService;
    }

    public override async Task OnConnectedAsync()
    {
        foreach (var waiter in await _waiterService.Get())
            await Clients.Client(Context.ConnectionId).SendAsync("OnWaiter", WaiterFactory.CreateDto(waiter));

        await base.OnConnectedAsync();
    }

    public async Task SendWaiter(WaiterDto waiter)
    {
        await Clients.All.SendAsync("OnWaiter", waiter);
    }
}