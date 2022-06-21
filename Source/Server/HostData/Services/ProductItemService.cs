using HostData.Domain.Contracts.Entities;
using HostData.Domain.Contracts.Models;
using HostData.Domain.Contracts.Services;
using HostData.Mapper;
using HostData.Repository;
using Shared.Data;
using Shared.Exceptions;

namespace HostData.Services;

public class ProductItemService : BaseService, IProductItemService
{
    public ProductItemService(IDbRepository dbRepository, IMapper mapper) : base(dbRepository, mapper)
    {
    }

    public async Task<Guid> Create(Guid entityThatChangesId, ProductItemModel product)
    {
        await CheckIfExists(product);
        return await base.Create<ProductItemModel, ProductItemEntity>(entityThatChangesId, product);
    }

    public async Task Delete(Guid id) =>
        await base.Delete<ProductItemEntity>(id);

    public async Task Update(Guid entityThatChangesId, ProductItemModel product)
    {
        await CheckIfExists(product);
        await base.Update<ProductItemModel, ProductItemEntity>(entityThatChangesId, product);
    }

    public async Task<ProductItemModel> GetById(Guid id) =>
        await base.GetById<ProductItemModel, ProductItemEntity>(id);

    public async Task<List<ProductItemModel>> Get() =>
        await base.Get<ProductItemModel, ProductItemEntity>();

    public async Task Remove(Guid entityThatChangesId, Guid id) =>
        await base.Remove<ProductItemEntity>(entityThatChangesId, id);

    private async Task CheckIfExists(ProductItemModel productItem)
    {
        var entity = Mapper.Map<ProductItemModel, ProductItemEntity>(productItem);
        if (await base.CheckIfExists(entity, x => x.Name.Equals(productItem.Name)) is true)
            throw new EntityAlreadyExistsException(productItem.Id, typeof(IProductItem).ToString());
    }
}