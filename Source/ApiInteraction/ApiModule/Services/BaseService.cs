using Microsoft.AspNetCore.SignalR.Client;
using Shared.Data;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace ApiModule.Services;

internal abstract class BaseService<TDto> where TDto : class
{
    public bool IsConnected => Connection.State == HubConnectionState.Connected;

    public HubConnection Connection { get; private set; }

    public event Action<EntityChangedEvent<TDto>>? ReceiveEvent;

    public BaseService(Uri url, int moduleLicenceId, IConfigSettings settings)
    {
        Connection ??= new HubConnectionBuilder().WithUrl(url, options =>
        {
            options.Headers.Add(nameof(LicenceDto.ModuleLicenceId), moduleLicenceId.ToString());
            options.Headers.Add(nameof(ConfigSettings.TerminalId), settings.TerminalId.ToString());
            options.Headers.Add(nameof(ConfigSettings.OrganizationId), settings.OrganizationId.ToString());
        }).Build();

        Connection.On<string>("ExceptionConnection", async (message) =>
        {
            await Disconnect();
            throw new InvalidLicenceModuleException(message);
        });
    }

    public virtual async Task Connect() =>
        await Connection.StartAsync();

    public virtual async Task Disconnect() =>
        await Connection.StopAsync();

    public async Task Send(string methodName, TDto dto, EventType eventType) =>
        await Connection.InvokeAsync(methodName, dto, eventType);

    protected void RaiseReceiveEvent(TDto dto, EventType eventType) =>
        ReceiveEvent?.Invoke(new EntityChangedEvent<TDto>(dto, eventType));
}