using HostData.Cache.Credentials;
using HostData.Controller.Contract;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using Shared.Data.Enum;
using Shared.Exceptions;
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
        if (CheckIfExistsPermission(waiterModel, pEnum) is true)
            throw new EntityAlreadyExistsException(waiterModel.Id, typeof(EmployeePermission).ToString());

        waiterModel.Permissions.Add(pEnum);
        await _waiterService.Update(entityThatChanges.Id, waiterModel);

        return Mapper.Map<WaiterModel, WaiterDto>(waiterModel);

        static bool CheckIfExistsPermission(WaiterModel model, EmployeePermission permission) =>
            model.Permissions.Any(x => x.HasFlag(permission));
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
        return Mapper.Map<WaiterModel, WaiterDto>(waiterModel);
    }

    public async Task<WaiterDto> GetWaiterById(dynamic waiterId)
    {
        var wId = (Guid)CheckDynamicGuid(waiterId);
        var waiterModel = await _waiterService.GetById(wId);
        return Mapper.Map<WaiterModel, WaiterDto>(waiterModel);
    }

    public async Task<WaiterModel> GetWaiterByPassword(dynamic password)
    {
        var p = (string)Convert.ToString(password);
        var waitersPermissionModel = await _waiterService.GetAll();
        return waitersPermissionModel.First(x => x.Password.Equals(p));
    }

    public async Task<List<WaiterDto>> GetWaiters()
    {
        var waiterModels = await _waiterService.GetAll();
        return waiterModels.Select(x => Mapper.Map<WaiterModel, WaiterDto>(x)).ToList();
    }

    public async Task<WaiterDto> RemovePermissionOnWaiterById(dynamic credentials, dynamic waiterId, dynamic permission)
    {
        var cId = (Guid)CheckDynamicGuid(credentials);
        var wId = (Guid)CheckDynamicGuid(waiterId);
        var pEnum = (EmployeePermission)Enum.Parse<EmployeePermission>(permission);
        var entityThatChanges = await CheckCredentials(cId);

        var waiterModel = await _waiterService.GetById(wId);
        if (waiterModel.Permissions.Remove(pEnum) is false)
            throw new EntityNotFoundException(waiterModel.Id, nameof(EmployeePermission).ToString());

        await _waiterService.Update(entityThatChanges.Id, waiterModel);

        return Mapper.Map<WaiterModel, WaiterDto>(waiterModel);
    }

    public async Task<WaiterDto> RemoveWaiterById(dynamic credentials, dynamic waiterId)
    {
        var cId = (Guid)CheckDynamicGuid(credentials);
        var wId = (Guid)CheckDynamicGuid(waiterId);

        var entityThatChanges = await CheckCredentials(cId);

        var waiterModel = await _waiterService.GetById(wId);

        await _waiterService.Remove(entityThatChanges.Id, wId);
        return Mapper.Map<WaiterModel, WaiterDto>(waiterModel);
    }

    public async Task<WaiterDto> OpenPersonalShift(dynamic credentials, dynamic waiterId)
    {
        var cId = (Guid)CheckDynamicGuid(credentials);
        var wId = (Guid)CheckDynamicGuid(waiterId);

        var entityThatChanges = await CheckCredentials(cId);

        var waiterModel = await _waiterService.GetById(wId);
        waiterModel.IsSessionOpen = true;

        await _waiterService.Update(entityThatChanges.Id, waiterModel);

        return Mapper.Map<WaiterModel, WaiterDto>(waiterModel);
    }

    public async Task<WaiterDto> ClosePersonalShift(dynamic credentials, dynamic waiterId)
    {
        var cId = (Guid)CheckDynamicGuid(credentials);
        var wId = (Guid)CheckDynamicGuid(waiterId);

        var entityThatChanges = await CheckCredentials(cId);

        var waiterModel = await _waiterService.GetById(wId);
        waiterModel.IsSessionOpen = false;

        await _waiterService.Update(entityThatChanges.Id, waiterModel);

        return Mapper.Map<WaiterModel, WaiterDto>(waiterModel);
    }
}