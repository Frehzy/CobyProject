using HostData.Cache.Credentials;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Mapper.Factory;
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

        var waiter = await WaiterService.GetById(waiterId);
        if (waiter.IsSessionOpen is false || waiter.IsDeleted is true)
            throw new WaiterDeletedOrPersonalSessionNotOpen(waiter.Id);

        return WaiterFactory.CreateDto(waiter);
    }

    protected async Task<WaiterDto> CheckPermission(Guid waiterId, EmployeePermission checkPermission)
    {
        var waiter = await WaiterService.GetById(waiterId);
        if (waiter.Permissions.Any(x => x.HasFlag(checkPermission)) is false)
            throw new PermissionDeniedException(checkPermission);

        return WaiterFactory.CreateDto(waiter);
    }
}