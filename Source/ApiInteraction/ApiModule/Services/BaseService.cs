using Microsoft.AspNetCore.SignalR.Client;
using Shared.Data.Enum;
using Shared.Factory.InternalModel;

namespace ApiModule.Services;

internal abstract class BaseService<TDto> where TDto : class
{
    public bool IsConnected => Connection.State == HubConnectionState.Connected;

    public HubConnection Connection { get; private set; }

    public event Action<EntityChangedEvent<TDto>>? ReceiveEvent;

    public BaseService(Uri url)
    {
        Connection ??= new HubConnectionBuilder().WithUrl(url).Build();
    }

    public async Task Connect() =>
        await Connection.StartAsync();

    public async Task Disconnect() =>
        await Connection.StopAsync();

    public async Task Send(string methodName, TDto dto, EventType eventType) =>
        await Connection.InvokeAsync(methodName, dto, eventType);

    protected void RaiseReceiveEvent(TDto dto, EventType eventType) =>
        ReceiveEvent?.Invoke(new EntityChangedEvent<TDto>(dto, eventType));
}