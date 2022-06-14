using HostData.Cache;
using Shared.Data;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal abstract class BaseController
{
    public IBaseCache<IWaiter> WaiterCache { get; }

    public BaseController(IBaseCache<IWaiter> waiterCache)
    {
        WaiterCache = waiterCache;
    }

    protected Guid CheckDynamicGuid(dynamic guid)
    {
        if (Guid.TryParse(guid.ToString(), out Guid returnGuid) is false)
            throw new ArgumentException($"{nameof(guid)} must be type Guid", nameof(guid));
        return returnGuid;
    }

    protected async Task<WaiterDto> CheckCredentials(Guid credentialsId, EmployeePermission checkPermission)
    {
        var waiter = WaiterCache.GetById(credentialsId);
        if (waiter.GetPermissions().Any(x => x.HasFlag(checkPermission)) is false)
            throw new PermissionDeniedException(checkPermission);

        if (waiter.IsSessionOpen is false || waiter.IsDeleted is true)
            throw new WaiterDeletedOrPersonalSessionNotOpen(waiter.Id);

        return WaiterFactory.CreateDto(waiter);
    }
}