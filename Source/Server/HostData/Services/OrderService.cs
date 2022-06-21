using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Enum;

namespace HostData.Services;

public class OrderService : BaseService, IOrderService
{
    public OrderService(IDbRepository dbRepository, IMapper mapper) : base(dbRepository, mapper)
    {
    }

    public async Task<Guid> Create(Guid entityThatChangesId, OrderModel order)
    {
        return await base.Create<OrderModel, OrderEntity>(entityThatChangesId, order);
    }

    public async Task Delete(Guid id) =>
        await base.Delete<OrderEntity>(id);

    public async Task Update(Guid entityThatChangesId, OrderModel order)
    {
        await base.Update<OrderModel, OrderEntity>(entityThatChangesId, order);
    }

    public async Task<OrderModel> GetById(Guid id)
    {
        return await base.GetById<OrderModel, OrderEntity>(id);
    }

    public async Task<List<OrderModel>> Get()
    {
        return await base.Get<OrderModel, OrderEntity>();
    }

    public async Task Remove(Guid entityThatChangesId, Guid id) =>
        await base.Remove<OrderEntity>(entityThatChangesId, id);

    public async Task<OrderModel> GetLastOrder()
    {
        var orderEntity = DbRepository.Context.Set<OrderEntity>().Last();
        return Mapper.Map<OrderEntity, OrderModel>(orderEntity);
    }

    public async Task<List<OrderModel>> GetOpenOrders()
    {
        var collection = await DbRepository.Context.Set<OrderEntity>()
                                                   .Where(x => x.IsDeleted == false && x.Status.HasFlag(OrderStatus.Open))
                                                   .ToListAsync();
        var models = Mapper.Map<OrderEntity, OrderModel>(collection);
        return models.ToList();
    }
}