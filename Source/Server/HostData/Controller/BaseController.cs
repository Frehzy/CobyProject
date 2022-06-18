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
    protected IMapper Mapper { get; }

    protected IWaiterPermissionService WaiterPermissionService { get; }

    protected ICacheCredentials CacheCredentials;

    public BaseController(IMapper mapper, IWaiterPermissionService waiterPermissionService, ICacheCredentials cacheCredentials)
    {
        Mapper = mapper;
        WaiterPermissionService = waiterPermissionService;
        CacheCredentials = cacheCredentials;
    }

    protected Guid CheckDynamicGuid(dynamic guid) =>
        Guid.TryParse(guid.ToString(), out Guid returnGuid) is true 
            ? returnGuid
            : throw new ArgumentException($"{nameof(guid)} must be type Guid", nameof(guid));

    protected async Task<WaiterDto> CheckCredentials(Guid credentialsId)
    {
        if (CacheCredentials.CheckCredentials(credentialsId, out Guid waiterId) is false)
            throw new InvalidSessionException();

        var waiter = await WaiterPermissionService.GetById(waiterId);
        if (waiter.Waiter.IsSessionOpen is false || waiter.Waiter.IsDeleted is true)
            throw new WaiterDeletedOrPersonalSessionNotOpen(waiter.Id);

        return WaiterFactory.CreateDto(waiter);
    }

    protected async Task<WaiterDto> CheckPermission(Guid waiterId, EmployeePermission checkPermission)
    {
        var waiter = await WaiterPermissionService.GetById(waiterId);
        if (waiter.Permissions.Any(x => x.EmployeePermission.HasFlag(checkPermission)) is false)
            throw new PermissionDeniedException(checkPermission);

        return WaiterFactory.CreateDto(waiter);
    }
}