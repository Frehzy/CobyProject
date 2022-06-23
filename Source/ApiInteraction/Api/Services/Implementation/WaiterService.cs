using Api.Services.Contrancts;
using Microsoft.AspNetCore.SignalR.Client;
using Shared.Factory.Dto;

namespace Api.Services.Implementation;

internal class WaiterService : BaseService<WaiterDto>, IWaiterService
{
    public WaiterService(Uri url) : base(new Uri(url, "waitersNotification"))
    {
        Connection.On<WaiterDto>("OnWaiter", (dto) => RaiseReceiveEvent(dto));
    }

    public async Task SendWaiter(WaiterDto waiter) =>
        await Send(nameof(SendWaiter), waiter);
}