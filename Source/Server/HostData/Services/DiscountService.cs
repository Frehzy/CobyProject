using HostData.Domain.Contracts.Entities.Order;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class DiscountService : BaseService, IDiscountService
{
    public DiscountService(IDbRepository dbRepository, IMapper mapper) : base(dbRepository, mapper)
    {
    }

    public async Task<Guid> Create(Guid entityThatChangesId, DiscountModel discount) =>
        await base.Create<DiscountModel, OrderDiscountEntity>(entityThatChangesId, discount);

    public async Task Delete(Guid id) =>
        await base.Delete<OrderDiscountEntity>(id);

    public async Task Update(Guid entityThatChangesId, DiscountModel discount) =>
        await base.Update<DiscountModel, OrderDiscountEntity>(entityThatChangesId, discount);

    public async Task<DiscountModel> GetById(Guid id) =>
        await base.GetById<DiscountModel, OrderDiscountEntity>(id);

    public async Task<List<DiscountModel>> GetAll() =>
        await base.GetAll<DiscountModel, OrderDiscountEntity>();

    public async Task Remove(Guid entityThatChangesId, Guid id) =>
        await base.Remove<OrderDiscountEntity>(entityThatChangesId, id);
}