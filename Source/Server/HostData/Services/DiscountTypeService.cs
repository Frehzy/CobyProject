using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;

namespace HostData.Services;

public class DiscountTypeService : BaseService, IDiscountTypeService
{
    public DiscountTypeService(IDbRepository dbRepository, IMapper mapper) : base(dbRepository, mapper)
    {
    }

    public async Task<Guid> Create(Guid entityThatChangesId, DiscountTypeModel discount) =>
        await base.Create<DiscountTypeModel, DiscountTypeEntity>(entityThatChangesId, discount);

    public async Task Delete(Guid id) =>
        await base.Delete<DiscountTypeEntity>(id);

    public async Task Update(Guid entityThatChangesId, DiscountTypeModel discount) =>
        await base.Update<DiscountTypeModel, DiscountTypeEntity>(entityThatChangesId, discount);

    public async Task<DiscountTypeModel> GetById(Guid id) =>
        await base.GetById<DiscountTypeModel, DiscountTypeEntity>(id);

    public async Task<List<DiscountTypeModel>> GetAll() =>
        await base.GetAll<DiscountTypeModel, DiscountTypeEntity>();

    public async Task Remove(Guid entityThatChangesId, Guid id) =>
        await base.Remove<DiscountTypeEntity>(entityThatChangesId, id);
}