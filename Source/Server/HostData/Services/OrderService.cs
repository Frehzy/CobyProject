using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Entities.Order;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class OrderService : BaseService, IOrderService
{
    public OrderService(IDbRepository dbRepository, IMapper mapper, OrderWaiterEntity waiterEntity)
        : base(dbRepository, mapper, waiterEntity)
    {
    }

    public async Task<Guid> Create(OrderModel order) =>
        await base.Create<OrderModel, OrderEntity>(order);

    public async Task Delete(Guid id) =>
        await base.Delete<OrderEntity>(id);

    public async Task Update(OrderModel order) =>
        await base.Update<OrderModel, OrderEntity>(order);

    public async Task<OrderModel> Get(Guid id) =>
        await base.GetById<OrderModel, OrderEntity>(id);

    public async Task<List<OrderModel>> GetAll() =>
        await base.GetAll<OrderModel, OrderEntity>();

    public async Task Remove(Guid id) =>
        await base.Remove<OrderEntity>(id);
}