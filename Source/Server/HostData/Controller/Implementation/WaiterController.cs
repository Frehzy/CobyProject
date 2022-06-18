using HostData.Cache.Credentials;
using HostData.Controller.Contract;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Mapper.Factory;
using Shared.Data.Enum;
using Shared.Factory.Dto;

namespace HostData.Controller.Implementation;

public class WaiterController : BaseController, IWaiterController
{
    private readonly IWaiterService _waiterService;

    public WaiterController(IWaiterService waiterService, IMapper mapper, IWaiterPermissionService waiterPermissionService, ICacheCredentials cacheCredentials) 
        : base(mapper, waiterPermissionService, cacheCredentials)
    {
        _waiterService = waiterService;
    }

    public async Task<WaiterDto> AddPermissionOnWaiterById(dynamic credentials, dynamic waiterId, dynamic permission)
    {
        var cId = (Guid)CheckDynamicGuid(credentials);
        var wId = (Guid)CheckDynamicGuid(waiterId);
        var pEnum = Enum.Parse<EmployeePermission>(permission);
        var entityThatChanges = await CheckCredentials(cId);

        var waiterModel = await _waiterService.WaiterPermissionService.GetById(wId);
        var permissions = await _waiterService.PermissionService.GetAll();
        var permissionModel = permissions.First(x => x.EmployeePermission.HasFlag(pEnum));

        waiterModel.Permissions.Add(permissionModel);
        await _waiterService.WaiterPermissionService.Update(entityThatChanges.Id, waiterModel);

        return WaiterFactory.CreateDto(waiterModel);
    }

    public async Task<WaiterDto> CreateWaiter(dynamic credentials, string name, string password)
    {
        var cId = (Guid)CheckDynamicGuid(credentials);
        var entityThatChanges = await CheckCredentials(cId);

        var waiterModel = new WaiterModel()
        {
            Name = name,
            Password = password,
            IsSessionOpen = false
        };
        await _waiterService.Create(entityThatChanges.Id, waiterModel);
        return WaiterFactory.CreateDto(waiterModel);
    }

    public async Task<WaiterDto> GetWaiterById(dynamic waiterId)
    {
        var wId = (Guid)CheckDynamicGuid(waiterId);
        var waiterModel = await WaiterPermissionService.GetById(wId);
        return WaiterFactory.CreateDto(waiterModel);
    }

    public async Task<WaiterPermissionModel> GetWaiterByPassword(string password)
    {
        var waitersPermissionModel = await WaiterPermissionService.GetAll();
        return waitersPermissionModel.First(x => x.Waiter.Password.Equals(password));
    }

    public async Task<List<WaiterDto>> GetWaiters()
    {
        var waitersPermissionModel = await WaiterPermissionService.GetAll();
        return waitersPermissionModel.Select(x => WaiterFactory.CreateDto(x)).ToList();
    }

    public async Task<WaiterDto> RemovePermissionOnWaiterById(dynamic credentials, dynamic waiterId, dynamic permission)
    {
        var cId = (Guid)CheckDynamicGuid(credentials);
        var wId = (Guid)CheckDynamicGuid(waiterId);
        var pEnum = Enum.Parse<EmployeePermission>(permission);
        var entityThatChanges = await CheckCredentials(cId);

        var waiterModel = await _waiterService.WaiterPermissionService.GetById(wId);
        var permissions = await _waiterService.PermissionService.GetAll();
        var permissionModel = permissions.First(x => x.EmployeePermission.HasFlag(pEnum));

        waiterModel.Permissions.Remove(permissionModel);
        await _waiterService.WaiterPermissionService.Update(entityThatChanges.Id, waiterModel);

        return WaiterFactory.CreateDto(waiterModel);
    }

    public async Task<WaiterDto> RemoveWaiter(dynamic credentials, dynamic waiterId)
    {
        var cId = (Guid)CheckDynamicGuid(credentials);
        var wId = (Guid)CheckDynamicGuid(waiterId);

        var entityThatChanges = await CheckCredentials(cId);

        var waiterModel = await _waiterService.GetById(wId);

        await _waiterService.Remove(entityThatChanges.Id, wId);
        return WaiterFactory.CreateDto(waiterModel); 
    }
}