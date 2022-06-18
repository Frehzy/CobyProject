using HostData.Domain.Contracts.Entities;
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
        await base.Create<DiscountModel, DiscountEntity>(entityThatChangesId, discount);

    public async Task Delete(Guid id) =>
        await base.Delete<DiscountEntity>(id);

    public async Task Update(Guid entityThatChangesId, DiscountModel discount) =>
        await base.Update<DiscountModel, DiscountEntity>(entityThatChangesId, discount);

    public async Task<DiscountModel> GetById(Guid id) =>
        await base.GetById<DiscountModel, DiscountEntity>(id);

    public async Task<List<DiscountModel>> GetAll() =>
        await base.GetAll<DiscountModel, DiscountEntity>();

    public async Task Remove(Guid entityThatChangesId, Guid id) =>
        await base.Remove<DiscountEntity>(entityThatChangesId, id);
}