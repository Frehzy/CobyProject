using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Entities.Order;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class DiscountService : BaseService, IDiscountService
{
    public DiscountService(IDbRepository dbRepository, IMapper mapper, OrderWaiterEntity connectEntity)
        : base(dbRepository, mapper, connectEntity)
    {
    }

    public async Task<Guid> Create(DiscountModel discount) =>
        await base.Create<DiscountModel, DiscountEntity>(discount);

    public async Task Delete(Guid id) =>
        await base.Delete<DiscountEntity>(id);

    public async Task Update(DiscountModel discount) =>
        await base.Update<DiscountModel, DiscountEntity>(discount);

    public async Task<DiscountModel> Get(Guid id) =>
        await base.GetById<DiscountModel, DiscountEntity>(id);

    public async Task<List<DiscountModel>> GetAll() =>
        await base.GetAll<DiscountModel, DiscountEntity>();

    public async Task Remove(Guid id) =>
        await base.Remove<DiscountEntity>(id);
}