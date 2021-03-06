using ApiHostData.Cache.Licence;
using ApiHostData.Controller.Contract;
using ApiHostData.Factory;
using ApiHostData.Services.Contract;
using Microsoft.AspNetCore.SignalR;
using Shared.Data.Enum;
using Shared.Factory.Dto;

namespace ApiHostData.Hubs;

public class WaiterHub : BaseHub
{
    private readonly IWaiterService _waiterService;

    public WaiterHub(IWaiterService waiterService, ILicenceCache licenceCache, ICredentialsController credentialsController)
        : base(licenceCache, credentialsController)
    {
        _waiterService = waiterService;
    }

    public override async Task OnConnectedAsync()
    {
        if (await base.CheckLicence() is false)
            return;
        else
        {
            foreach (var waiter in await _waiterService.Get())
                await Clients.Client(Context.ConnectionId).SendAsync("OnWaiter", WaiterFactory.CreateDto(waiter), EventType.Updated);

            await base.OnConnectedAsync();
        }
    }

    public async Task SendWaiter(WaiterDto waiter, EventType eventType)
    {
        await Clients.All.SendAsync("OnWaiter", waiter, eventType);
    }
}