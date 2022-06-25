using ApiModule.Services.Contrancts;
using Microsoft.AspNetCore.SignalR.Client;
using Shared.Data;
using Shared.Data.Enum;
using Shared.Factory.Dto;

namespace ApiModule.Services.Implementation;

internal class WaiterService : BaseService<WaiterDto>, IWaiterService
{
    public WaiterService(Uri url, int moduleLicenceId, IConfigSettings settings) 
        : base(new Uri(url, "waitersNotification"), moduleLicenceId, settings)
    {
        Connection.On<WaiterDto, EventType>("OnWaiter", (dto, eventType) => RaiseReceiveEvent(dto, eventType));
    }

    public async Task SendWaiter(WaiterDto waiter, EventType eventType) =>
        await Send(nameof(SendWaiter), waiter, eventType);
}