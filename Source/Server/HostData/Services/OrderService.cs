using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class OrderService : BaseService, IOrderService
{
    public OrderService(IDbRepository dbRepository, IMapper mapper) : base(dbRepository, mapper)
    {
    }

    public async Task<Guid> Create(Guid entityThatChangesId, OrderModel order) =>
        await base.Create<OrderModel, OrderEntity>(entityThatChangesId, order);

    public async Task Delete(Guid id) =>
        await base.Delete<OrderEntity>(id);

    public async Task Update(Guid entityThatChangesId, OrderModel order) =>
        await base.Update<OrderModel, OrderEntity>(entityThatChangesId, order);

    public async Task<OrderModel> GetById(Guid id) =>
        await base.GetById<OrderModel, OrderEntity>(id);

    public async Task<List<OrderModel>> GetAll() =>
        await base.GetAll<OrderModel, OrderEntity>();

    public async Task Remove(Guid entityThatChangesId, Guid id) =>
        await base.Remove<OrderEntity>(entityThatChangesId, id);
}