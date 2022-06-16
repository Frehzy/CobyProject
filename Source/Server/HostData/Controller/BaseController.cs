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

    public BaseController(IMapper mapper, IWaiterPermissionService waiterPermissionService)
    {
        Mapper = mapper;
        WaiterPermissionService = waiterPermissionService;
    }

    protected Guid CheckDynamicGuid(dynamic guid)
    {
        if (Guid.TryParse(guid.ToString(), out Guid returnGuid) is false)
            throw new ArgumentException($"{nameof(guid)} must be type Guid", nameof(guid));
        return returnGuid;
    }

    internal async Task<WaiterDto> CheckCredentials(Guid credentialsId, EmployeePermission checkPermission)
    {
        var waiter = await WaiterPermissionService.GetById(credentialsId);
        if (waiter.Permissions.Any(x => x.EmployeePermission.HasFlag(checkPermission)) is false)
            throw new PermissionDeniedException(checkPermission);

        if (waiter.Waiter.IsSessionOpen is false || waiter.Waiter.IsDeleted is true)
            throw new WaiterDeletedOrPersonalSessionNotOpen(waiter.Id);

        return WaiterFactory.CreateDto(waiter);
    }
}