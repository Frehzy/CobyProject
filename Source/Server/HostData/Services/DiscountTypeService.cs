using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;
using Shared.Data;
using Shared.Exceptions;

namespace HostData.Services;

public class DiscountTypeService : BaseService, IDiscountTypeService
{
    public DiscountTypeService(IDbRepository dbRepository, IMapper mapper) : base(dbRepository, mapper)
    {
    }

    public async Task<Guid> Create(Guid entityThatChangesId, DiscountTypeModel discount)
    {
        await CheckIfExists(discount);
        return await base.Create<DiscountTypeModel, DiscountTypeEntity>(entityThatChangesId, discount);
    }

    public async Task Delete(Guid id) =>
        await base.Delete<DiscountTypeEntity>(id);

    public async Task Update(Guid entityThatChangesId, DiscountTypeModel discount)
    {
        await CheckIfExists(discount);
        await base.Update<DiscountTypeModel, DiscountTypeEntity>(entityThatChangesId, discount);
    }

    public async Task<DiscountTypeModel> GetById(Guid id) =>
        await base.GetById<DiscountTypeModel, DiscountTypeEntity>(id);

    public async Task<List<DiscountTypeModel>> Get() =>
        await base.Get<DiscountTypeModel, DiscountTypeEntity>();

    public async Task Remove(Guid entityThatChangesId, Guid id) =>
        await base.Remove<DiscountTypeEntity>(entityThatChangesId, id);

    private async Task CheckIfExists(DiscountTypeModel discountType)
    {
        var entity = Mapper.Map<DiscountTypeModel, DiscountTypeEntity>(discountType);
        if (await base.CheckIfExists(entity, x => x.Name.Equals(discountType.Name)) is true)
            throw new EntityAlreadyExistsException(discountType.Id, typeof(IDiscountType).ToString());
    }
}