using ApiHostData.Cache.Licence;
using ApiHostData.Controller.Contract;
using Microsoft.AspNetCore.SignalR;
using Shared.Data;
using Shared.Exceptions;
using Shared.Factory.Dto;

namespace ApiHostData.Hubs;

public abstract class BaseHub : Hub
{
    private readonly ILicenceCache _licenceCache;
    private readonly ICredentialsController _credentialsController;

    public BaseHub(ILicenceCache licenceCache, ICredentialsController credentialsController)
    {
        _licenceCache = licenceCache;
        _credentialsController = credentialsController;
    }

    public virtual async Task<bool> CheckLicence()
    {
        var httpContext = Context.GetHttpContext();
        httpContext.Request.Headers.TryGetValue(nameof(LicenceDto.ModuleLicenceId), out var moduleLicenceId);
        httpContext.Request.Headers.TryGetValue(nameof(IConfigSettings.TerminalId), out var terminalId);
        httpContext.Request.Headers.TryGetValue(nameof(IConfigSettings.OrganizationId), out var organizationId);

        var licences = await _credentialsController.CheckLicence(organizationId, moduleLicenceId);
        if (licences.Count > 0 && _licenceCache.AddLicence(terminalId, new LicenceDto(new Guid(organizationId), Convert.ToInt32(moduleLicenceId), licences.First().MaxReservedLicence)) is true)
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