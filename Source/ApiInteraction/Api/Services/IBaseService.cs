using Microsoft.AspNetCore.SignalR.Client;

namespace Api.Services;

internal interface IBaseService<TDto>
{
    public bool IsConnected { get; }

    public HubConnection Connection { get; }

    event Action<TDto>? ReceiveEvent;

    Task Connect();

    Task Disconnect();
}