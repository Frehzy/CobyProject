using Microsoft.AspNetCore.SignalR.Client;
using Shared.Factory.InternalModel;

namespace Api.Services;

internal interface IBaseService<TDto> where TDto : class
{
    public bool IsConnected { get; }

    public HubConnection Connection { get; }

    event Action<EntityChangedEvent<TDto>>? ReceiveEvent;

    Task Connect();

    Task Disconnect();
}