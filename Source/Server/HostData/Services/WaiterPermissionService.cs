using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Entities.Order;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;
using System.Linq;

namespace HostData.Services;

public class WaiterPermissionService : BaseService, IWaiterPermissionService
{
    private readonly IWaiterService _waiterService;
    private readonly IPermissionService _permissionService;

    public WaiterPermissionService(IDbRepository dbRepository, IMapper mapper, OrderWaiterEntity connectEntity, IWaiterService waiterService, IPermissionService permissionService)
        : base(dbRepository, mapper, connectEntity)
    {
        _waiterService = waiterService;
        _permissionService = permissionService;
    }

    public async Task<Guid> Create(WaiterPermissionModel waiter) =>
        await base.Create<WaiterPermissionModel, WaiterPermissionEntity>(waiter);

    public async Task Delete(Guid id) =>
        await base.Delete<WaiterPermissionEntity>(id);

    public async Task Update(WaiterPermissionModel waiter) =>
        await base.Update<WaiterPermissionModel, WaiterPermissionEntity>(waiter);

    public async Task<WaiterPermissionModel> GetById(Guid id) =>
        await base.GetById<WaiterPermissionModel, WaiterPermissionEntity>(id);

    public async Task<List<WaiterPermissionModel>> GetAll()
    {
        var waiters = await _waiterService.GetAll();
        var waitersPermissions = await base.GetAll<WaiterPermissionModel, WaiterPermissionEntity>();
        return waiters.Select(waiter => new WaiterPermissionModel()
        {
            Id = waiter.Id,
            Waiter = waiter,
            Permissions = (waitersPermissions?.Where(x => x.Id.Equals(waiter.Id)).SelectMany(x => x.Permissions) ?? Enumerable.Empty<PermissionModel>()).ToList()
        }).ToList();
    }

    public async Task Remove(Guid id) =>
        await base.Remove<WaiterPermissionEntity>(id);
}