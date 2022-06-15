using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Entities.Order;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class PermissionService : BaseService, IPermissionService
{
    public PermissionService(IDbRepository dbRepository, IMapper mapper, OrderWaiterEntity connectEntity) : base(dbRepository, mapper, connectEntity)
    {
    }

    public async Task<Guid> Create(PermissionModel permission) =>
        await base.Create<PermissionModel, PermissionEntity>(permission);

    public async Task Delete(Guid id) =>
        await base.Delete<PermissionEntity>(id);

    public async Task Update(PermissionModel permission) =>
        await base.Update<PermissionModel, PermissionEntity>(permission);

    public async Task<PermissionModel> Get(Guid id) =>
        await base.GetById<PermissionModel, PermissionEntity>(id);

    public async Task<List<PermissionModel>> GetAll() =>
        await base.GetAll<PermissionModel, PermissionEntity>();

    public async Task Remove(Guid id) =>
        await base.Remove<PermissionEntity>(id);
}