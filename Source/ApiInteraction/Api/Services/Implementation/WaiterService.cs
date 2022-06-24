using Api.Services.Contrancts;
using Microsoft.AspNetCore.SignalR.Client;
using Shared.Data.Enum;
using Shared.Factory.Dto;

namespace Api.Services.Implementation;

internal class WaiterService : BaseService<WaiterDto>, IWaiterService
{
    public WaiterService(Uri url) : base(new Uri(url, "waitersNotification"))
    {
        Connection.On<WaiterDto, EventType>("OnWaiter", (dto, eventType) => RaiseReceiveEvent(dto, eventType));
    }

    public async Task SendWaiter(WaiterDto waiter, EventType eventType) =>
        await Send(nameof(SendWaiter), waiter, eventType);
}