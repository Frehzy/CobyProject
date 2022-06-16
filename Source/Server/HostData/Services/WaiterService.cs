using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Entities.Order;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class WaiterService : BaseService, IWaiterService
{
    public WaiterService(IDbRepository dbRepository, IMapper mapper, OrderWaiterEntity connectEntity) 
        : base(dbRepository, mapper, connectEntity)
    {
    }

    public async Task<Guid> Create(WaiterModel waiter) =>
        await base.Create<WaiterModel, WaiterEntity>(waiter);

    public async Task Delete(Guid id) =>
        await base.Delete<WaiterEntity>(id);

    public async Task Update(WaiterModel waiter) =>
        await base.Update<WaiterModel, WaiterEntity>(waiter);

    public async Task<WaiterModel> GetById(Guid id) =>
        await base.GetById<WaiterModel, WaiterEntity>(id);

    public async Task<List<WaiterModel>> GetAll() =>
        await base.GetAll<WaiterModel, WaiterEntity>();

    public async Task Remove(Guid id) =>
        await base.Remove<WaiterEntity>(id);
}