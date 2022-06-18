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

    public WaiterController(IWaiterService waiterService, IMapper mapper, ICredentialsCache cacheCredentials) 
        : base(waiterService, mapper, cacheCredentials)
    {
        _waiterService = waiterService;
    }

    public async Task<WaiterDto> AddPermissionOnWaiterById(dynamic credentials, dynamic waiterId, dynamic permission)
    {
        var cId = (Guid)CheckDynamicGuid(credentials);
        var wId = (Guid)CheckDynamicGuid(waiterId);
        var pEnum = (EmployeePermission)Enum.Parse<EmployeePermission>(permission);
        var entityThatChanges = await CheckCredentials(cId);

        var waiterModel = await _waiterService.GetById(wId);
        waiterModel.Permissions.Add(pEnum);
        await _waiterService.Update(entityThatChanges.Id, waiterModel);

        return WaiterFactory.CreateDto(waiterModel);
    }

    public async Task<WaiterDto> CreateWaiter(dynamic credentials, dynamic name, dynamic password)
    {
        var cId = (Guid)CheckDynamicGuid(credentials);
        var n = (string)Convert.ToString(name);
        var p = (string)Convert.ToString(password);
        var entityThatChanges = await CheckCredentials(cId);

        var waiterModel = new WaiterModel()
        {
            Name = n,
            Password = p,
            IsSessionOpen = false
        };
        await _waiterService.Create(entityThatChanges.Id, waiterModel);
        return WaiterFactory.CreateDto(waiterModel);
    }

    public async Task<WaiterDto> GetWaiterById(dynamic waiterId)
    {
        var wId = (Guid)CheckDynamicGuid(waiterId);
        var waiterModel = await _waiterService.GetById(wId);
        return WaiterFactory.CreateDto(waiterModel);
    }

    public async Task<WaiterModel> GetWaiterByPassword(dynamic password)
    {
        var p = (string)Convert.ToString(password);
        var waitersPermissionModel = await _waiterService.GetAll();
        return waitersPermissionModel.First(x => x.Password.Equals(p));
    }

    public async Task<List<WaiterDto>> GetWaiters()
    {
        var waitersPermissionModel = await _waiterService.GetAll();
        return waitersPermissionModel.Select(x => WaiterFactory.CreateDto(x)).ToList();
    }

    public async Task<WaiterDto> RemovePermissionOnWaiterById(dynamic credentials, dynamic waiterId, dynamic permission)
    {
        var cId = (Guid)CheckDynamicGuid(credentials);
        var wId = (Guid)CheckDynamicGuid(waiterId);
        var pEnum = (EmployeePermission)Enum.Parse<EmployeePermission>(permission);
        var entityThatChanges = await CheckCredentials(cId);

        var waiterModel = await _waiterService.GetById(wId);
        waiterModel.Permissions.Remove(pEnum);
        await _waiterService.Update(entityThatChanges.Id, waiterModel);

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