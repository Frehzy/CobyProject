using ApiHostData.Domain.Entities;
using ApiHostData.Domain.Models;
using ApiHostData.Repository;
using ApiHostData.Services.Contract;
using Shared.Data;
using Shared.Exceptions;
using SharedData.Mapper;

namespace ApiHostData.Services.Implementation;

public class ProductItemService : BaseService, IProductItemService
{
    public ProductItemService(IApiHostRepository dbRepository, IMapper mapper) : base(dbRepository, mapper)
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