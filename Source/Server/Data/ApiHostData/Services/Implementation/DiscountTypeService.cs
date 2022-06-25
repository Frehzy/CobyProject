using ApiHostData.Domain.Entities;
using ApiHostData.Domain.Models;
using ApiHostData.Repository;
using ApiHostData.Services.Contract;
using Shared.Data;
using Shared.Exceptions;
using SharedData.Mapper;

namespace ApiHostData.Services.Implementation;

public class DiscountTypeService : BaseService, IDiscountTypeService
{
    public DiscountTypeService(IApiHostRepository dbRepository, IMapper mapper) : base(dbRepository, mapper)
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