using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Entities.Order;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class WaiterService : BaseService, IWaiterService
{
    public IWaiterPermissionService WaiterPermissionService { get; }

    public IPermissionService PermissionService { get; }

    public WaiterService(IWaiterPermissionService waiterPermissionService, IPermissionService permissionService, IDbRepository dbRepository, IMapper mapper)
        : base(dbRepository, mapper)
    {
        WaiterPermissionService = waiterPermissionService;
        PermissionService = permissionService;
    }

    public async Task<Guid> Create(Guid entityThatChangesId, WaiterModel waiter) =>
        await base.Create<WaiterModel, WaiterEntity>(entityThatChangesId, waiter);

    public async Task Delete(Guid id) =>
        await base.Delete<WaiterEntity>(id);

    public async Task Update(Guid entityThatChangesId, WaiterModel waiter) =>
        await base.Update<WaiterModel, WaiterEntity>(entityThatChangesId, waiter);

    public async Task<WaiterModel> GetById(Guid id) =>
        await base.GetById<WaiterModel, WaiterEntity>(id);

    public async Task<List<WaiterModel>> GetAll() =>
        await base.GetAll<WaiterModel, WaiterEntity>();

    public async Task Remove(Guid entityThatChangesId, Guid id)
    {
        await base.Remove<WaiterEntity>(entityThatChangesId, id);
        await WaiterPermissionService.Remove(entityThatChangesId, id);
    }
}