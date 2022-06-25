using ApiHostData.Cache.Credentials;
using ApiHostData.Domain.Models;
using ApiHostData.Factory;
using ApiHostData.Services.Contract;
using Shared.Data.Enum;
using Shared.Exceptions;
using Shared.Factory.Dto;
using SharedData.Mapper;

namespace ApiHostData.Controller;

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

    protected async Task CheckIfOrderIsClosed(OrderModel order)
    {
        if (order.Status is not OrderStatus.Open)
            throw new CantChangeAndRemoveOrderException(order.Id);
    }

    protected async Task<WaiterDto> CheckPermission(Guid waiterId, EmployeePermission checkPermission)
    {
        var waiterModel = await WaiterService.GetById(waiterId);
        if (waiterModel.Permissions.Any(x => x.HasFlag(checkPermission)) is false)
            throw new PermissionDeniedException(checkPermission);

        return WaiterFactory.CreateDto(waiterModel);
    }
}