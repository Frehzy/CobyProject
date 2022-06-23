using Microsoft.AspNetCore.SignalR.Client;

namespace Api.Services;

internal abstract class BaseService<TDto>
{
    public bool IsConnected => Connection.State == HubConnectionState.Connected;

    public HubConnection Connection { get; private set; }

    public event Action<TDto>? ReceiveEvent;

    public BaseService(Uri url)
    {
        Connection ??= new HubConnectionBuilder().WithUrl(url).Build();
    }

    public async Task Connect() =>
        await Connection.StartAsync();

    public async Task Disconnect() =>
        await Connection.StopAsync();

    public async Task Send(string methodName, TDto dto) =>
        await Connection.InvokeAsync(methodName, dto);

    protected void RaiseReceiveEvent(TDto dto) =>
        ReceiveEvent?.Invoke(dto);
}