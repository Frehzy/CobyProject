using ApiHostData.Cache.Licence;
using Microsoft.AspNetCore.SignalR;
using Shared.Data;
using Shared.Exceptions;
using Shared.Factory.Dto;

namespace ApiHostData.Hubs;

public abstract class BaseHub : Hub
{
    private readonly ILicenceCache _licenceCache;

    public BaseHub(ILicenceCache licenceCache)
    {
        _licenceCache = licenceCache;
    }

    public virtual async Task<bool> CheckLicence()
    {
        var httpContext = Context.GetHttpContext();
        httpContext.Request.Headers.TryGetValue(nameof(LicenceDto.ModuleLicenceId), out var moduleLicenceId);
        httpContext.Request.Headers.TryGetValue(nameof(IConfigSettings.TerminalId), out var terminalId);
        httpContext.Request.Headers.TryGetValue(nameof(IConfigSettings.OrganizationId), out var organizationId);

        if (_licenceCache.AddLicence(Convert.ToInt32(moduleLicenceId), terminalId, organizationId) is true)
            return true;
        else
            await Clients.Client(Context.ConnectionId).SendAsync("ExceptionConnection", nameof(InvalidLicenceModuleException));
        return false;
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Context.GetHttpContext().Request.Headers.TryGetValue(nameof(IConfigSettings.TerminalId), out var terminalId);
        _licenceCache.RemoveLicence(terminalId);
        return base.OnDisconnectedAsync(exception);
    }
}