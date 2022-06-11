using HostData.Cache.Waiters;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory;
using Shared.Factory.Dto;

namespace HostData.Controllers;

internal abstract class BaseController
{
    private readonly IWaiterCache _waiterCache;

    public BaseController(IWaiterCache waiterCache)
    {
        _waiterCache = waiterCache;
    }

    protected Guid CheckDynamicGuid(dynamic guid)
    {
        if (Guid.TryParse(guid.ToString(), out Guid returnGuid) is false)
            throw new ArgumentException($"{nameof(guid)} must be type Guid", nameof(guid));
        return returnGuid;
    }

    protected WaiterDto CheckCredentials(Guid credentialsId, EmployeePermission checkPermission)
    {
        var waiter = _waiterCache.GetWaiterById(credentialsId);
        if (waiter.GetPermissions().Any(x => x.HasFlag(checkPermission)) is false)
            throw new PermissionDeniedException(checkPermission);

        return WaiterFactory.CreateDto(waiter);
    }
}