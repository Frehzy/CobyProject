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
    public WaiterPermissionService(IDbRepository dbRepository, IMapper mapper) : base(dbRepository, mapper)
    {
    }

    public async Task<Guid> Create(Guid entityThatChangesId, WaiterPermissionModel waiter) =>
        await base.Create<WaiterPermissionModel, WaiterPermissionEntity>(entityThatChangesId, waiter);

    public async Task Delete(Guid id) =>
        await base.Delete<WaiterPermissionEntity>(id);

    public async Task Update(Guid entityThatChangesId, WaiterPermissionModel waiter) =>
        await base.Update<WaiterPermissionModel, WaiterPermissionEntity>(entityThatChangesId, waiter);

    public async Task<WaiterPermissionModel> GetById(Guid id)
    {
        var waiter = await base.GetById<WaiterModel, WaiterEntity>(id);
        var waitersPermission = await base.GetById<WaiterPermissionModel, WaiterPermissionEntity>(id);
        return new WaiterPermissionModel()
        {
            Id = waiter.Id,
            Waiter = waiter,
            Permissions = waitersPermission.Permissions
        };
    }

    public async Task<List<WaiterPermissionModel>> GetAll()
    {
        var waiters = await base.GetAll<WaiterModel, WaiterEntity>();
        var waitersPermissions = await base.GetAll<WaiterPermissionModel, WaiterPermissionEntity>();
        return waiters.Select(waiter => new WaiterPermissionModel()
        {
            Id = waiter.Id,
            Waiter = waiter,
            Permissions = (waitersPermissions?.Where(x => x.Id.Equals(waiter.Id)).SelectMany(x => x.Permissions) ?? Enumerable.Empty<PermissionModel>()).ToList()
        }).ToList();
    }

    public async Task Remove(Guid entityThatChangesId, Guid id) =>
        await base.Remove<WaiterPermissionEntity>(entityThatChangesId, id);
}