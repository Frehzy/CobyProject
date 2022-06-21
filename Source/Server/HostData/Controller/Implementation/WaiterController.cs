using HostData.Cache.Credentials;
using HostData.Controller.Contract;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Factory;
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
        Guid cId = CheckDynamicGuid(credentials);
        Guid wId = CheckDynamicGuid(waiterId);
        EmployeePermission pEnum = Enum.Parse<EmployeePermission>(permission);
        var entityThatChanges = await CheckCredentials(cId);

        var waiterModel = await _waiterService.GetById(wId);
        if (CheckIfExistsPermission(waiterModel, pEnum) is true)
            throw new EntityAlreadyExistsException(waiterModel.Id, typeof(EmployeePermission).ToString());

        waiterModel.Permissions.Add(pEnum);
        await _waiterService.Update(entityThatChanges.Id, waiterModel);

        return WaiterFactory.CreateDto(waiterModel);

        static bool CheckIfExistsPermission(WaiterModel model, EmployeePermission permission) =>
            model.Permissions.Any(x => x.HasFlag(permission));
    }

    public async Task<WaiterDto> CreateWaiter(dynamic credentials, dynamic name, dynamic password)
    {
        Guid cId = CheckDynamicGuid(credentials);
        string n = Convert.ToString(name.ToString());
        string p = Convert.ToString(password.ToString());
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
        Guid wId = CheckDynamicGuid(waiterId);
        var waiterModel = await _waiterService.GetById(wId);
        return WaiterFactory.CreateDto(waiterModel);
    }

    public async Task<WaiterModel> GetWaiterByPassword(dynamic password)
    {
        string p = Convert.ToString(password.ToString());
        var waitersPermissionModel = await _waiterService.Get();
        return waitersPermissionModel.First(x => x.Password.Equals(p));
    }

    public async Task<List<WaiterDto>> GetWaiters()
    {
        var waiterModels = await _waiterService.Get();
        return waiterModels.Select(x => WaiterFactory.CreateDto(x)).ToList();
    }

    public async Task<WaiterDto> RemovePermissionOnWaiterById(dynamic credentials, dynamic waiterId, dynamic permission)
    {
        Guid cId = CheckDynamicGuid(credentials);
        Guid wId = CheckDynamicGuid(waiterId);
        EmployeePermission pEnum = Enum.Parse<EmployeePermission>(permission);
        var entityThatChanges = await CheckCredentials(cId);

        var waiterModel = await _waiterService.GetById(wId);
        if (waiterModel.Permissions.Remove(pEnum) is false)
            throw new EntityNotFoundException(waiterModel.Id, nameof(EmployeePermission).ToString());

        await _waiterService.Update(entityThatChanges.Id, waiterModel);

        return WaiterFactory.CreateDto(waiterModel);
    }

    public async Task<WaiterDto> RemoveWaiterById(dynamic credentials, dynamic waiterId)
    {
        Guid cId = CheckDynamicGuid(credentials);
        Guid wId = CheckDynamicGuid(waiterId);

        var entityThatChanges = await CheckCredentials(cId);

        var waiterModel = await _waiterService.GetById(wId);

        await _waiterService.Remove(entityThatChanges.Id, wId);
        return WaiterFactory.CreateDto(waiterModel);
    }

    public async Task<WaiterDto> OpenPersonalShift(dynamic credentials, dynamic waiterId)
    {
        Guid cId = CheckDynamicGuid(credentials);
        Guid wId = CheckDynamicGuid(waiterId);

        var waiterModel = await WaiterService.GetById(wId);
        var entityThatChanges = WaiterFactory.CreateDto(waiterModel);

        waiterModel.IsSessionOpen = true;

        await _waiterService.Update(entityThatChanges.Id, waiterModel);

        return WaiterFactory.CreateDto(waiterModel);
    }

    public async Task<WaiterDto> ClosePersonalShift(dynamic credentials, dynamic waiterId)
    {
        Guid cId = CheckDynamicGuid(credentials);
        Guid wId = CheckDynamicGuid(waiterId);

        var waiterModel = await WaiterService.GetById(wId);
        var entityThatChanges = WaiterFactory.CreateDto(waiterModel);

        waiterModel.IsSessionOpen = false;

        await _waiterService.Update(entityThatChanges.Id, waiterModel);

        return WaiterFactory.CreateDto(waiterModel);
    }
}