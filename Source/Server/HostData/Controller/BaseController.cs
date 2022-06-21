using HostData.Cache.Credentials;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Factory;
using HostData.Mapper;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory.Dto;

namespace HostData.Controller;

public class BaseController
{
    protected IWaiterService WaiterService { get; }

    protected IMapper Mapper { get; }

    protected ICredentialsCache CredentialsCache { get; }

    public BaseController(IWaiterService waiterService, IMapper mapper, ICredentialsCache credentialsCache)
    {
        WaiterService = waiterService;
        Mapper = mapper;
        CredentialsCache = credentialsCache;
    }

    protected Guid CheckDynamicGuid(dynamic guid) =>
        Guid.TryParse(guid.ToString(), out Guid returnGuid) is true
            ? returnGuid
            : throw new ArgumentException($"{nameof(guid)} must be type Guid", nameof(guid));

    protected async Task<WaiterDto> CheckCredentials(Guid credentialsId)
    {
        if (CredentialsCache.CheckCredentials(credentialsId, out Guid waiterId) is false)
            throw new InvalidSessionException();

        var waiterModel = await WaiterService.GetById(waiterId);
        if (waiterModel.IsSessionOpen is false || waiterModel.IsDeleted is true)
            throw new WaiterDeletedOrPersonalSessionNotOpen(waiterModel.Id);

        return WaiterFactory.CreateDto(waiterModel);
    }

    protected async Task<WaiterDto> CheckPermission(Guid waiterId, EmployeePermission checkPermission)
    {
        var waiterModel = await WaiterService.GetById(waiterId);
        if (waiterModel.Permissions.Any(x => x.HasFlag(checkPermission)) is false)
            throw new PermissionDeniedException(checkPermission);

        return WaiterFactory.CreateDto(waiterModel);
    }
}